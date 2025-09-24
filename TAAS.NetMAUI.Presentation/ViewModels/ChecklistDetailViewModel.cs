using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Business.Services;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Presentation.Data;
using TAAS.NetMAUI.Presentation.Models;
using Task = System.Threading.Tasks.Task;

namespace TAAS.NetMAUI.Presentation.ViewModels {

    public partial class ChecklistDetailViewModel : ObservableObject {

        private readonly IServiceManager _manager;


        [ObservableProperty]
        private ChecklistItem checklist;

        [ObservableProperty]
        private ObservableCollection<ChecklistDetailGroupItem> checklistGrouped = new ObservableCollection<ChecklistDetailGroupItem>();

        [ObservableProperty]
        private ObservableCollection<DisplayPhotoItem> fileList;
        [ObservableProperty]
        private ObservableCollection<UploadedFileItem> uploadedFileList;

        [ObservableProperty]
        private ObservableCollection<ChecklistHeaderDto> headerList;


        [ObservableProperty]
        private bool isEditable;


        public bool HasChecklistFiles => FileList != null && FileList.Any();
        public bool HasUploadedFiles => UploadedFileList != null && UploadedFileList.Any();


        public bool HasChecklistHeaders => HeaderList != null && HeaderList.Any();

        public bool HasChecklistDetails => ChecklistGrouped != null && ChecklistGrouped.Any();


        public ChecklistDetailViewModel( IServiceManager manager ) {
            _manager = manager;
            Checklist = NavigationContext.CurrentChecklist;
        }

        public async System.Threading.Tasks.Task LoadChecklistFilesAsync() {
            //Files
            var allChecklistTaasFiles = await _manager.ChecklistTaasFileService.GetByChecklistId( Checklist.Id, false );
            var allowedExtensions = new HashSet<string>( StringComparer.OrdinalIgnoreCase )
            {
                ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif",
                ".webp", ".heic", ".heif", ".raw", ".svg", ".ico", ".jfif"
            };

            this.FileList = new ObservableCollection<DisplayPhotoItem>(
                allChecklistTaasFiles
                    .Where( cf => allowedExtensions.Contains( Path.GetExtension( cf.TaasFile.Name ) ) )
                    .Select( f => new DisplayPhotoItem {
                        Id = f.Id,
                        PhotoImage = ImageSource.FromStream( () => new MemoryStream( f.TaasFile.FileData ) )
                    } )
            );

            OnPropertyChanged( nameof( HasChecklistFiles ) );

            //Uploaded Files
            this.UploadedFileList = new ObservableCollection<UploadedFileItem>(
                allChecklistTaasFiles
                    .Where( cf => !allowedExtensions.Contains( Path.GetExtension( cf.TaasFile.Name ) ) )
                    .Select( f => new UploadedFileItem {
                        Id = f.Id,
                        FileName = f.TaasFile.Name,
                        Extension = Path.GetExtension( f.TaasFile.Name ),
                        Size = FileSizeConverter.Convert( f.TaasFile.Size ),
                    } )
            );

            OnPropertyChanged( nameof( HasUploadedFiles ) );


        }

        public async System.Threading.Tasks.Task LoadChecklistHeadersAndDetailsAsync() {
            //Headers
            var allChecklistHeaders = await _manager.ChecklistHeaderService.GetByChecklistId( Checklist.Id, false );
            HeaderList = new ObservableCollection<ChecklistHeaderDto>( allChecklistHeaders );

            OnPropertyChanged( nameof( HasChecklistHeaders ) );

            //Details
            ChecklistGrouped.Clear();
            var allChecklistDetails = await _manager.ChecklistDetailService.GetByChecklistId( Checklist.Id, false );
            foreach ( var checklistDetail in allChecklistDetails ) {
                if ( checklistDetail.ChecklistTemplateDetail.Title.HasValue && checklistDetail.ChecklistTemplateDetail.Title.Value ) {

                    var nextTitle = allChecklistDetails.FirstOrDefault( c => c.ChecklistTemplateDetail.Title.HasValue
                    && c.ChecklistTemplateDetail.Title.Value && c.ChecklistTemplateDetail.Sequence > checklistDetail.ChecklistTemplateDetail.Sequence );


                    ChecklistGrouped.Add( new ChecklistDetailGroupItem() {
                        Master = checklistDetail,
                        Details = new ObservableCollection<DetailQuestionItem>( ChecklistDetailDtoToDetailQuestionItemConverter.Convert( allChecklistDetails.Where( d =>
                        ( !d.ChecklistTemplateDetail.Title.HasValue || !d.ChecklistTemplateDetail.Title.Value )
                        && d.ChecklistTemplateDetail.Sequence > checklistDetail.ChecklistTemplateDetail.Sequence
                        && ( ( nextTitle != null && d.ChecklistTemplateDetail.Sequence < nextTitle.ChecklistTemplateDetail.Sequence ) || nextTitle == null ) ) ) )
                    } );
                }

            }

            OnPropertyChanged( nameof( HasChecklistDetails ) );
        }

        public void EvaluatePermissions() {
            var sessionUserId = Preferences.Get( "SessionUserId", -1L );

            IsEditable = sessionUserId > 0 &&
                         Checklist?.ChecklistAuditors?.Any( a => a.AuditorId == sessionUserId ) == true;
        }


        private async Task SaveFileAndLoad( FileResult result ) {
            try {

                using var stream = await result.OpenReadAsync();
                using var ms = new MemoryStream();
                await stream.CopyToAsync( ms );
                var bytes = ms.ToArray();

                var sessionUserId = Preferences.Get( "SessionUserId", -1L );
                var sessionUser = await _manager.AuditorService.GetById( sessionUserId, false );

                var taasFile = new TaasFileCreateDto {
                    Name = result.FileName,
                    Size = result.ContentType.Length,
                    FileData = bytes,
                    CreatedBy = $"{sessionUser.FirstName} {sessionUser.LastName}",
                    CreatedDate = DateTime.Now,
                };

                ChecklistTaasFileCreateDto checklistTaasFile = new ChecklistTaasFileCreateDto() {
                    ChecklistId = Checklist.Id,
                    TaasFile = taasFile
                };

                await _manager.ChecklistTaasFileService.Create( checklistTaasFile );

                await LoadChecklistFilesAsync();
            }
            catch ( Exception ex ) {
                throw new Exception( ex.Message );
            }
        }

        [RelayCommand]
        public async System.Threading.Tasks.Task TakePhotoAsync() {
            try {
                FileResult photo = await MediaPicker.CapturePhotoAsync();

                if ( photo != null )
                    await SaveFileAndLoad( photo );

            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[TakePhotoAsync] {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Photo could not be saved.", "OK" );
            }
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task DeletePhotoAsync( DisplayPhotoItem displayPhoto ) {
            if ( displayPhoto == null )
                return;

            bool isConfirmed = await Shell.Current.DisplayAlert(
                "Confirm Deletion",
                "Are you sure you want to delete this photo?",
                "Yes", "No" );

            if ( !isConfirmed )
                return;

            try {
                var checklistTaasFile = await _manager.ChecklistTaasFileService.GetById( displayPhoto.Id, true );

                var checklistDetailTaasFile = await _manager.ChecklistDetailTaasFileService.GetByTaasFileId( checklistTaasFile.TaasFileId, false );

                if ( checklistDetailTaasFile.Any() )
                    await Shell.Current.DisplayAlert( "Warning", "This file is related to one or more questions.", "OK" );
                else {
                    await _manager.ChecklistTaasFileService.SoftDelete( new ChecklistTaasFileDeleteDto() { Id = checklistTaasFile.Id }, true );
                    FileList.Remove( displayPhoto );
                    await Shell.Current.DisplayAlert( "Deleted", "Photo has been deleted.", "OK" );
                }
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[DeletePhotoAsync] {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to delete photo.", "OK" );
            }
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task UploadFileAsync() {
            try {
                var result = await FilePicker.PickAsync( new PickOptions {
                    PickerTitle = "Select a file"
                } );

                if ( result != null )
                    await SaveFileAndLoad( result );

            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[UploadFileAsync] {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "File could not be uploaded.", "OK" );
            }
        }

        [RelayCommand]
        private async Task DeleteFileAsync( UploadedFileItem uploadedFile ) {
            if ( uploadedFile == null )
                return;

            bool isConfirmed = await Shell.Current.DisplayAlert(
                "Confirm Deletion",
                "Are you sure you want to delete this photo?",
                "Yes", "No" );

            if ( !isConfirmed )
                return;

            try {
                var checklistTaasFile = await _manager.ChecklistTaasFileService.GetById( uploadedFile.Id, true );

                var checklistDetailTaasFile = await _manager.ChecklistDetailTaasFileService.GetByTaasFileId( checklistTaasFile.TaasFileId, false );

                if ( checklistDetailTaasFile.Any() )
                    await Shell.Current.DisplayAlert( "Warning", "This file is related to one or more questions.", "OK" );
                else {
                    await _manager.ChecklistTaasFileService.SoftDelete( new ChecklistTaasFileDeleteDto() { Id = checklistTaasFile.Id }, true );
                    this.UploadedFileList.Remove( uploadedFile );
                    await Shell.Current.DisplayAlert( "Deleted", "File has been deleted.", "OK" );
                }
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[DeletePhotoAsync] {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to delete photo.", "OK" );
            }
        }


        [RelayCommand]
        private async System.Threading.Tasks.Task SaveHeaderValueAsync( ChecklistHeaderDto header ) {
            if ( header == null || Checklist == null ) return;

            await _manager.ChecklistHeaderService.Update( header.Id, new ChecklistHeaderUpdateDto() { Id = header.Id, Value = header.Value }, true );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task SaveDetailAnswerValueAsync( DetailQuestionItem detail ) {
            if ( detail == null || Checklist == null ) return;

            await _manager.ChecklistDetailService.Update( detail.Id, new ChecklistDetailAnswerUpdateDto() { Id = detail.Id, Answer = detail.Answer }, true );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task SaveDetailExplanationValueAsync( DetailQuestionItem detail ) {
            if ( detail == null || Checklist == null ) return;

            await _manager.ChecklistDetailService.Update( detail.Id, new ChecklistDetailExplanationUpdateDto() { Id = detail.Id, Explanation = detail.Explanation }, true );
        }
        [RelayCommand]
        private async Task ShowQuestionDetailAsync( DetailQuestionItem detail ) {
            NavigationContext.ChecklistDetailId = detail.Id;
            await Shell.Current.GoToAsync( nameof( QuestionDetailPage ) );
        }
    }
}
