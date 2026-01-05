using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Presentation.Models;
using TAAS.NetMAUI.Presentation.ViewModels;

namespace TAAS.NetMAUI.Presentation;

public partial class ChecklistDetailPage : ContentPage {
    public ChecklistDetailPage( ChecklistDetailViewModel model ) {
        InitializeComponent();
        BindingContext = model;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();

        if ( BindingContext is ChecklistDetailViewModel vm ) {
            await vm.LoadChecklistHeadersAndDetailsAsync();
            await vm.LoadChecklistFilesAsync();
            await vm.EvaluatePermissions();
        }
    }

    private void HeaderValue_Unfocused( object sender, FocusEventArgs e ) {
        if ( sender is Entry entry && entry.BindingContext is ChecklistHeaderDto header &&
            BindingContext is ChecklistDetailViewModel vm ) {
            vm.SaveHeaderValueCommand.Execute( header );
        }
    }

    private void DetailAnswer_CheckedChanged( object sender, CheckedChangedEventArgs e ) {

        if ( !e.Value ) return;

        if ( sender is RadioButton radioButton &&
            radioButton.BindingContext is DetailQuestionItem detail &&
            BindingContext is ChecklistDetailViewModel vm ) {
            string selected = radioButton.Value.ToString();


            bool hasChanged = !detail.IsTouched || detail.Answer != selected;

            if ( !hasChanged )
                return;

            detail.IsTouched = true;
            detail.Answer = selected;

            if ( vm.SaveDetailAnswerValueCommand.CanExecute( detail ) )
                vm.SaveDetailAnswerValueCommand.Execute( detail );

        }
    }

    private void DetailExplanation_Unfocused( object sender, FocusEventArgs e ) {
        if ( sender is Entry entry && entry.BindingContext is DetailQuestionItem detail &&
            BindingContext is ChecklistDetailViewModel vm ) {
            vm.SaveDetailExplanationValueCommand.Execute( detail );
        }
    }
}