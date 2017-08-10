namespace DataTemplateCreation
{
    // If we don't make this class public, we get this error when registering data template:
    // "Only public classes can be used in markup. 'TextViewModelBase' type is not public."
    public class TextViewModel : TextViewModelBase
    {
    }
}
