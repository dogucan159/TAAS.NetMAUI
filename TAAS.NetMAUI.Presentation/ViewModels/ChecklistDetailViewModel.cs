using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Presentation.Data;
using TAAS.NetMAUI.Presentation.Models;
using TAAS.NetMAUI.Shared;
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

        [ObservableProperty]
        private bool isUndoFinalizeButtonVisible;

        //[ObservableProperty]
        //private bool isApproveButtonVisible;

        //[ObservableProperty]
        //private bool isUndoApproveButtonVisible;


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

        public async Task EvaluatePermissions() {

            var auditorDto = await _manager.AuditorService.GetByMachineName( false );

            //bool isReviewer = auditorDto != null && Checklist?.ReviewedAuditor?.Id == auditorDto.Id;
            bool isPreparer = auditorDto != null && Checklist?.ChecklistAuditors?.Any( a => a.AuditorId == auditorDto.Id ) == true;

            IsEditable = isPreparer && Checklist?.Status == ChecklistStatusConst.INITIAL;

            IsUndoFinalizeButtonVisible = isPreparer && Checklist?.Status == ChecklistStatusConst.FINALIZED;

            //IsApproveButtonVisible = isReviewer && Checklist?.Status == ChecklistStatusConst.FINALIZED;

            //IsUndoApproveButtonVisible = isReviewer && Checklist?.Status == ChecklistStatusConst.APPROVED;
        }


        private async Task SaveFileAndLoad( FileResult result ) {
            try {

                byte[] bytes = [];
                using var stream = await result.OpenReadAsync();

                /*TODO: Remove BTGM formul iceren excel dosyalarinin upload edilmesine izin verdi. if ( IsExcel( result ) ) {

                    var validationOptions = new ExcelValidationOptions() {
                        StopOnFirstCritical = true,
                        AddFormulaToViolation = true,
                        AddFormulaLikeTextToViolation = true
                    };
                    var validation = await ExcelUploadValidator.ValidateAsync( stream, validationOptions );
                    if ( !validation.IsValid )
                        throw new Exception( "The uploaded Excel file contains disallowed content and cannot be accepted." );
                    else
                        bytes = validation.Bytes;

                }
                else {
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync( ms );
                    bytes = ms.ToArray();
                }*/
                using var ms = new MemoryStream();
                await stream.CopyToAsync( ms );
                bytes = ms.ToArray();

                var auditorDto = await _manager.AuditorService.GetByMachineName( false );

                var sessionUser = await _manager.AuditorService.GetById( auditorDto.Id, false );

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
            catch ( FeatureNotSupportedException ) {
                await Shell.Current.DisplayAlert( "Error",
                    "Camera capture is not supported on this device.",
                    "OK" );
            }
            catch ( Exception ex ) {
                await Shell.Current.DisplayAlert( "Error", $"Photo could not be saved: {ex.Message}", "OK" );
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
        private async System.Threading.Tasks.Task DownloadPhotoAsync( DisplayPhotoItem displayPhoto ) {

            try {

                var checklistTaasFile = await _manager.ChecklistTaasFileService.GetById( displayPhoto.Id, true );
                var fileName = $"{checklistTaasFile.TaasFile.Name}";
                var filePath = Path.Combine( FileSystem.AppDataDirectory, fileName );

                File.WriteAllBytes( filePath, checklistTaasFile.TaasFile.FileData );

                await Launcher.OpenAsync( new OpenFileRequest {
                    File = new ReadOnlyFile( filePath )
                } );
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[DownloadPhotoAsync] {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to download photo.", "OK" );
            }
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task UploadFileAsync() {
            try {

                var customFileType = new FilePickerFileType(
                                new Dictionary<DevicePlatform, IEnumerable<string>>
                                {
                                    { DevicePlatform.WinUI, new[] { ".csv",
                                    ".doc",
                                    ".docx",
                                    ".jpeg",
                                    ".jpg",
                                    ".pdf",
                                    ".png",
                                    ".ppt",
                                    ".pptx",
                                    ".tiff",
                                    ".txt",
                                    ".xls",
                                    ".xlsx",
                                    ".xml",
                                    ".zip"
                                        } }
                                } );

                var result = await FilePicker.PickAsync( new PickOptions {
                    PickerTitle = "Select a file",
                    FileTypes = customFileType
                } );

                if ( result != null ) {

                    /*if ( !await ValidateMimeTypeAsync( result ) ) {
                        await Shell.Current.DisplayAlert( "Invalid File", "The selected file's content does not match its file type. Please select a valid file.", "OK" );
                        return;
                    }
                    else
                        await SaveFileAndLoad( result );*/

                    await SaveFileAndLoad( result );
                }


            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[UploadFileAsync] {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", $"File could not be uploaded. Reason: {ex.Message}", "OK" );
            }
        }

        private async Task<bool> ValidateMimeTypeAsync( FileResult file ) {
            try {
                using var stream = await file.OpenReadAsync();

                string actualMimeType = await GetMimeTypeFromFileSignatureAsync( stream );
                string expectedMimeType = GetMimeTypeFromExtension( file.FileName );

                // Special handling for text-based formats that don't have magic numbers
                if ( IsTextBasedExtension( file.FileName ) ) {
                    // For text files, verify it's actually text content
                    return actualMimeType.StartsWith( "text/" ) || actualMimeType == "application/octet-stream";
                }

                // For binary formats, we need a clear match
                return actualMimeType == expectedMimeType ||
                       IsMimeTypeCompatible( actualMimeType, expectedMimeType );
            }
            catch {
                return false;
            }
        }

        private bool IsTextBasedExtension( string fileName ) {
            string extension = Path.GetExtension( fileName ).ToLowerInvariant();
            return extension == ".csv" || extension == ".txt" || extension == ".xml";
        }

        private async Task<string> GetMimeTypeFromFileSignatureAsync( Stream stream ) {
            byte[] buffer = new byte[512];
            int bytesRead = await stream.ReadAsync( buffer, 0, buffer.Length );
            stream.Position = 0;

            if ( bytesRead < 4 )
                return "application/octet-stream";

            // Image formats
            // JPEG
            if ( buffer[0] == 0xFF && buffer[1] == 0xD8 && buffer[2] == 0xFF )
                return "image/jpeg";

            // PNG
            if ( buffer[0] == 0x89 && buffer[1] == 0x50 && buffer[2] == 0x4E &&
                buffer[3] == 0x47 && buffer[4] == 0x0D && buffer[5] == 0x0A &&
                buffer[6] == 0x1A && buffer[7] == 0x0A )
                return "image/png";

            // TIFF (Little Endian)
            if ( buffer[0] == 0x49 && buffer[1] == 0x49 && buffer[2] == 0x2A && buffer[3] == 0x00 )
                return "image/tiff";

            // TIFF (Big Endian)
            if ( buffer[0] == 0x4D && buffer[1] == 0x4D && buffer[2] == 0x00 && buffer[3] == 0x2A )
                return "image/tiff";

            // PDF
            if ( buffer[0] == 0x25 && buffer[1] == 0x50 && buffer[2] == 0x44 && buffer[3] == 0x46 )
                return "application/pdf";

            // ZIP and Office formats
            if ( buffer[0] == 0x50 && buffer[1] == 0x4B && buffer[2] == 0x03 && buffer[3] == 0x04 ) {
                return DetectOfficeFormat( buffer, bytesRead );
            }

            // Old Office formats (DOC, XLS, PPT)
            if ( buffer[0] == 0xD0 && buffer[1] == 0xCF && buffer[2] == 0x11 &&
                buffer[3] == 0xE0 && buffer[4] == 0xA1 && buffer[5] == 0xB1 &&
                buffer[6] == 0x1A && buffer[7] == 0xE1 )
                return "application/msoffice-legacy";

            // Check if it's a text-based format
            if ( IsTextBasedFormat( buffer, bytesRead ) ) {
                return DetectTextFormat( buffer, bytesRead );
            }

            return "application/octet-stream";
        }

        private string DetectOfficeFormat( byte[] buffer, int bytesRead ) {
            string content = System.Text.Encoding.ASCII.GetString( buffer, 0, Math.Min( bytesRead, 512 ) );

            if ( content.Contains( "word/" ) )
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            if ( content.Contains( "xl/" ) )
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            if ( content.Contains( "ppt/" ) )
                return "application/vnd.openxmlformats-officedocument.presentationml.presentation";

            return "application/zip";
        }

        private bool IsTextBasedFormat( byte[] buffer, int bytesRead ) {
            int sampleSize = Math.Min( bytesRead, 512 );
            int textCharCount = 0;
            int totalChars = 0;

            for ( int i = 0; i < sampleSize; i++ ) {
                byte b = buffer[i];
                totalChars++;

                // Null byte indicates binary
                if ( b == 0 )
                    return false;

                // Check for UTF-8 BOM
                if ( i == 0 && b == 0xEF && bytesRead > 2 && buffer[1] == 0xBB && buffer[2] == 0xBF ) {
                    textCharCount += 3;
                    continue;
                }

                // Count printable characters and common whitespace
                if ( ( b >= 0x20 && b <= 0x7E ) || // Printable ASCII
                    b == 0x09 || // Tab
                    b == 0x0A || // Line feed
                    b == 0x0D || // Carriage return
                    b >= 0x80 )   // Extended ASCII/UTF-8
                {
                    textCharCount++;
                }
            }

            // If at least 95% of characters are text-like, consider it text
            double textRatio = ( double )textCharCount / totalChars;
            return textRatio >= 0.95;
        }

        private string DetectTextFormat( byte[] buffer, int bytesRead ) {
            string content = System.Text.Encoding.UTF8.GetString( buffer, 0, Math.Min( bytesRead, 512 ) ).TrimStart();

            // XML detection
            if ( content.StartsWith( "<?xml", StringComparison.OrdinalIgnoreCase ) ||
                content.StartsWith( "<", StringComparison.OrdinalIgnoreCase ) )
                return "text/xml";

            // CSV detection - look for delimiter patterns
            var lines = content.Split( new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries );
            if ( lines.Length > 0 ) {
                string firstLine = lines[0];
                // Count commas - if there are multiple commas, likely CSV
                int commaCount = firstLine.Count( c => c == ',' );
                if ( commaCount >= 1 ) {
                    // Additional validation: check if subsequent lines have similar comma count
                    if ( lines.Length > 1 ) {
                        int secondLineCommas = lines[1].Count( c => c == ',' );
                        // If comma counts are similar, it's likely CSV
                        if ( Math.Abs( commaCount - secondLineCommas ) <= 1 )
                            return "text/csv";
                    }
                    else {
                        // Single line with commas - assume CSV
                        return "text/csv";
                    }
                }
            }

            return "text/plain";
        }

        private string GetMimeTypeFromExtension( string fileName ) {
            string extension = Path.GetExtension( fileName ).ToLowerInvariant();

            return extension switch {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".tiff" => "image/tiff",
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".doc" => "application/msword",
                ".xls" => "application/vnd.ms-excel",
                ".ppt" => "application/vnd.ms-powerpoint",
                ".zip" => "application/zip",
                ".xml" => "text/xml",
                ".csv" => "text/csv",
                ".txt" => "text/plain",
                _ => "application/octet-stream"
            };
        }

        private bool IsMimeTypeCompatible( string actual, string expected ) {
            if ( actual == expected )
                return true;

            // Legacy Office formats - all use OLE/CFB signature (0xD0CF11E0A1B11AE1)
            // We need to accept any of these when we detect the legacy format
            if ( actual == "application/msoffice-legacy" &&
                ( expected == "application/msword" ||
                 expected == "application/vnd.ms-excel" ||
                 expected == "application/vnd.ms-powerpoint" ) )
                return true;

            // Modern Office formats
            if ( actual == "application/zip" &&
                ( expected == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                 expected == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ||
                 expected == "application/vnd.openxmlformats-officedocument.presentationml.presentation" ||
                 expected == "application/zip" ) )
                return true;

            if ( actual == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" &&
                ( expected == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                 expected == "application/zip" ) )
                return true;

            if ( actual == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" &&
                ( expected == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ||
                 expected == "application/zip" ) )
                return true;

            if ( actual == "application/vnd.openxmlformats-officedocument.presentationml.presentation" &&
                ( expected == "application/vnd.openxmlformats-officedocument.presentationml.presentation" ||
                 expected == "application/zip" ) )
                return true;

            // Text format compatibility
            if ( actual == "text/plain" && expected == "text/plain" )
                return true;

            if ( actual == "text/csv" && expected == "text/csv" )
                return true;

            if ( actual == "text/xml" && expected == "text/xml" )
                return true;

            return false;
        }

        /*TODO: Remove BTGM formul iceren excel dosyalarinin upload edilmesine izin verdi. private bool IsExcel( FileResult file ) {
            var ext = Path.GetExtension( file.FileName )?.ToLowerInvariant();
            if ( ext == ".xlsx" || ext == ".xls" || ext == ".csv" ) return true;

            var mime = file.ContentType?.ToLowerInvariant() ?? "";
            return mime.Contains( "spreadsheet" ) || mime.Contains( "excel" );
        }*/




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
        [RelayCommand]
        private async System.Threading.Tasks.Task NavigateToMainPage() {
            await Shell.Current.GoToAsync( "//MainPage" );
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task NavigateToChecklistPage() {
            await Shell.Current.GoToAsync( nameof( ChecklistPage ) );
        }

        [RelayCommand]
        private async Task DownloadFileAsync( UploadedFileItem uploadedFile ) {

            try {

                var checklistTaasFile = await _manager.ChecklistTaasFileService.GetById( uploadedFile.Id, true );
                var fileName = $"{checklistTaasFile.TaasFile.Name}";
                var filePath = Path.Combine( FileSystem.AppDataDirectory, fileName );

                File.WriteAllBytes( filePath, checklistTaasFile.TaasFile.FileData );

                await Launcher.OpenAsync( new OpenFileRequest {
                    File = new ReadOnlyFile( filePath )
                } );
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[DownloadFileAsync] {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to download file.", "OK" );
            }
        }


        private async Task UpdateStatus() {
            try {
                await _manager.ChecklistService.Update( NavigationContext.CurrentChecklist.Id, new ChecklistUpdateDto() {
                    Id = NavigationContext.CurrentChecklist.Id,
                    Status = Checklist.Status
                }, true );
                //await EvaluatePermissions();
                await NavigateToChecklistPage();
            }
            catch ( Exception ex ) {

                throw new Exception( ex.Message );
            }
        }


        [RelayCommand]
        private async Task FinalizeChecklistAsync() {
            try {

                var checklistDetails = await _manager.ChecklistDetailService.GetByChecklistId( Checklist.Id, false );

                bool hasMatch = checklistDetails?.Any( c => c.ChecklistTemplateDetail.Title != true && string.IsNullOrEmpty( c.Answer ) ) ?? false;

                if ( hasMatch ) {
                    await Shell.Current.DisplayAlert( "Warning", "All questions must be answered before finalizing the checklist.", "OK" );
                    return;
                }


                bool isConfirmed = await Shell.Current.DisplayAlert(
                    "Confirm Finalization",
                    "Are you sure you want to finalize the checklist?",
                    "Yes", "No" );

                if ( !isConfirmed )
                    return;


                //var auditorDto = _manager.AuditorService.GetByMachineName( false ).Result;

                //bool isReviewer = auditorDto != null && Checklist?.ReviewedAuditor?.Id == auditorDto.Id;
                //bool isPreparer = auditorDto != null && Checklist?.ChecklistAuditors?.Any( a => a.AuditorId == auditorDto.Id ) == true;

                Checklist.Status = ChecklistStatusConst.FINALIZED;

                await this.UpdateStatus();

            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[FinalizeChecklistAsync] {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to finalize checklist.", "OK" );
            }
        }

        [RelayCommand]
        private async Task UndoFinalizeChecklistAsync() {
            try {
                bool isConfirmed = await Shell.Current.DisplayAlert(
                    "Confirm Withdraw",
                    "Are you sure you want to undo the finalization of the checklist?",
                    "Yes", "No" );

                if ( !isConfirmed )
                    return;

                Checklist.Status = ChecklistStatusConst.INITIAL;
                await this.UpdateStatus();
            }
            catch ( Exception ex ) {
                Debug.WriteLine( $"[UndoFinalizeChecklistAsync] {ex.Message}" );
                await Shell.Current.DisplayAlert( "Error", "Failed to undo checklist.", "OK" );
            }
        }

        //[RelayCommand]
        //private async Task ApproveChecklistAsync() {
        //    try {

        //        bool isConfirmed = await Shell.Current.DisplayAlert(
        //            "Confirm Deletion",
        //            "Are you sure you want to approve the checklist?",
        //            "Yes", "No" );

        //        if ( !isConfirmed )
        //            return;

        //        Checklist.Status = "A";
        //        await this.UpdateStatus();
        //    }
        //    catch ( Exception ex ) {
        //        Debug.WriteLine( $"[ApproveChecklistAsync] {ex.Message}" );
        //        await Shell.Current.DisplayAlert( "Error", "Failed to approve checklist.", "OK" );
        //    }
        //}

        //[RelayCommand]
        //private async Task UndoApproveChecklistAsync() {
        //    try {

        //        bool isConfirmed = await Shell.Current.DisplayAlert(
        //            "Confirm Deletion",
        //            "Are you sure you want to undo the approval of the checklist?",
        //            "Yes", "No" );

        //        if ( !isConfirmed )
        //            return;

        //        var auditorDto = _manager.AuditorService.GetByMachineName( false ).Result;

        //        bool isReviewer = auditorDto != null && Checklist?.ReviewedAuditor?.Id == auditorDto.Id;
        //        bool isPreparer = auditorDto != null && Checklist?.ChecklistAuditors?.Any( a => a.AuditorId == auditorDto.Id ) == true;

        //        Checklist.Status = isReviewer && isPreparer ? ChecklistStatusConst.INITIAL : ChecklistStatusConst.FINALIZED;
        //        await this.UpdateStatus();
        //    }
        //    catch ( Exception ex ) {
        //        Debug.WriteLine( $"[UndoApproveChecklistAsync] {ex.Message}" );
        //        await Shell.Current.DisplayAlert( "Error", "Failed to undo checklist.", "OK" );
        //    }
        //}

    }
}
