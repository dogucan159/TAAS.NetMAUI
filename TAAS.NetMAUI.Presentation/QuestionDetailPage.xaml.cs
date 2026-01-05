using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using TAAS.NetMAUI.Business.Interfaces;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Core.Entities;
using TAAS.NetMAUI.Presentation.Data;
using TAAS.NetMAUI.Presentation.Models;

namespace TAAS.NetMAUI.Presentation;

public partial class QuestionDetailPage : ContentPage {

    private readonly IServiceManager _manager;

    private string _pendingHtmlContent;

    private bool IsTappedBorderReadOnly;

    private IEnumerable<ChecklistDetailTaasFileDto> checklistDetailTaasFiles;

    public ObservableCollection<TaasFileItem> FileList = new ObservableCollection<TaasFileItem>();


    public QuestionDetailPage( IServiceManager manager ) {

        InitializeComponent();

        EditorWebView.Navigated += async ( s, e ) => {
            if ( !string.IsNullOrEmpty( _pendingHtmlContent ) ) {
                string escapedHtml = _pendingHtmlContent
                    .Replace( "\\", "\\\\" )
                    .Replace( "'", "\\'" )
                    .Replace( "\"", "\\\"" )
                    .Replace( "\r", "" )
                    .Replace( "\n", "\\n" );

                string js = $"window.setContent(`{escapedHtml}`);";
                await EditorWebView.EvaluateJavaScriptAsync( js );
            }
        };

        _manager = manager;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();

        var checklistTaasFiles = await _manager.ChecklistTaasFileService.GetByChecklistId( NavigationContext.CurrentChecklist.Id, false );

        this.checklistDetailTaasFiles = await _manager.ChecklistDetailTaasFileService.GetByChecklistDetailId( NavigationContext.ChecklistDetailId.Value, true );

        this.FileList = new ObservableCollection<TaasFileItem>( this.ConvertTaasFileToTaasFileItem( checklistTaasFiles.Select( c => c.TaasFile ), this.checklistDetailTaasFiles ) );

        FilesCollectionView.ItemsSource = FileList;

        await LoadEditorHtmlAsync();

        var auditorDto = await _manager.AuditorService.GetByMachineName( false );

        bool isPreparer = auditorDto != null && NavigationContext.CurrentChecklist?.ChecklistAuditors?.Any( a => a.AuditorId == auditorDto.Id ) == true;

        SaveButton.IsVisible = isPreparer && ( NavigationContext.CurrentChecklist?.Status == "I" || NavigationContext.CurrentChecklist?.Status == "PF" );

    }

    private List<TaasFileItem> ConvertTaasFileToTaasFileItem( IEnumerable<TaasFileDto> taasFiles, IEnumerable<ChecklistDetailTaasFileDto> checklistDetailTaasFiles ) {

        List<TaasFileItem> lstResult = new List<TaasFileItem>();
        foreach ( var taasFile in taasFiles ) {
            lstResult.Add( new TaasFileItem() {
                Id = taasFile.Id,
                Name = taasFile.Name,
                Size = FileSizeConverter.Convert( taasFile.Size ),
                Extension = Path.GetExtension( taasFile.Name ),
                IsSelected = checklistDetailTaasFiles.Any( c => c.TaasFileId == taasFile.Id )
            } );
        }
        return lstResult;
    }


    private async System.Threading.Tasks.Task LoadEditorHtmlAsync() {
        ChecklistDetailDto checklistDetail = await _manager.ChecklistDetailService.GetById( NavigationContext.ChecklistDetailId.Value, true );

        if ( !string.IsNullOrWhiteSpace( checklistDetail.ExplanationFormatted ) ) {
            _pendingHtmlContent = checklistDetail.ExplanationFormatted;
        }

    }

    private async Task<string> GetEditorContentAsync() {
        var result = await EditorWebView.EvaluateJavaScriptAsync( "window.getContent();" );

        var normalizedResult = Regex.Unescape( result );

        return normalizedResult;
    }

    private async void OnSaveClicked( object sender, EventArgs e ) {
        string editorHtml = await GetEditorContentAsync();

        await _manager.ChecklistDetailService.Update( NavigationContext.ChecklistDetailId.Value, new ChecklistDetailExplanationFormattedUpdateDto() {
            Id = NavigationContext.ChecklistDetailId.Value,
            ExplanationFormatted = editorHtml
        }, true );
        //Created
        if ( this.FileList != null && this.FileList.Count > 0 ) {
            List<ChecklistDetailTaasFileCreateDto> lstCreated = new List<ChecklistDetailTaasFileCreateDto>();
            List<ChecklistDetailTaasFileDeleteDto> lstDeleted = new List<ChecklistDetailTaasFileDeleteDto>();

            foreach ( var item in this.FileList ) {
                if ( item.IsSelected && !this.checklistDetailTaasFiles.Any( c => c.TaasFileId == item.Id ) )
                    lstCreated.Add( new ChecklistDetailTaasFileCreateDto() { ChecklistDetailId = NavigationContext.ChecklistDetailId.Value, TaasFileId = item.Id } );
            }

            //Deleted
            if ( this.checklistDetailTaasFiles != null && this.checklistDetailTaasFiles.Count() > 0 ) {
                foreach ( var item in this.checklistDetailTaasFiles ) {
                    if ( !this.FileList.Any( f => f.Id == item.TaasFileId && f.IsSelected ) )
                        lstDeleted.Add( new ChecklistDetailTaasFileDeleteDto() { Id = item.Id } );
                }
            }
            if ( lstCreated.Any() )
                await _manager.ChecklistDetailTaasFileService.CreateList( lstCreated );
            if ( lstDeleted.Any() )
                await _manager.ChecklistDetailTaasFileService.SoftDeleteList( lstDeleted, true );
        }

        await Shell.Current.GoToAsync( nameof( ChecklistDetailPage ) );
    }

    private void OnBorderTapped( object sender, EventArgs e ) {
        if ( sender is Border border && border.BindingContext is TaasFileItem item ) {
            item.IsSelected = !item.IsSelected;
        }
    }


    private async void OnMainPageTapped( object sender, EventArgs e ) {
        await Shell.Current.GoToAsync( "//MainPage" );
    }

    private async void OnChecklistPageTapped( object sender, EventArgs e ) {
        await Shell.Current.GoToAsync( nameof( ChecklistPage ) );
    }

    private async void OnChecklistDetailPageTapped( object sender, EventArgs e ) {
        await Shell.Current.GoToAsync( nameof( ChecklistDetailPage ) );
    }

}