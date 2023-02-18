
using CommunityToolkit.Mvvm.ComponentModel;

namespace CommunityToolkitDemo;

public partial class TestConfig : ObservableObject
{
    [ObservableProperty]
    private bool? enabled=false;
    [ObservableProperty]
    private bool? enabled2=false;
    [ObservableProperty]
    private string? name1;
    [ObservableProperty]
    private string? name2;
    [ObservableProperty]
    private string? name3;
    [ObservableProperty]
    private string? name4;
    [ObservableProperty]
    private string? name5;
    [ObservableProperty]
    private string? name6;
    [ObservableProperty]
    private string? name7;
    [ObservableProperty]
    private string? name8;
    [ObservableProperty]
    private string? name9;
    [ObservableProperty]
    private string? name10;
    [ObservableProperty]
    private string? name11;
    [ObservableProperty]
    private string? name12;
    [ObservableProperty]
    private string? name13;
    [ObservableProperty]
    private string? name14;
    [ObservableProperty]
    private string? name15;
    [ObservableProperty]
    private string? name16;
    [ObservableProperty]
    private string? name17;
    [ObservableProperty]
    private string? name18;
    [ObservableProperty]
    private string? name19;
    [ObservableProperty]
    private string? name20;

    partial void OnName1Changing(string? value)
    {
        Console.WriteLine($"Name is about to change to {value}");
    }

    partial void OnName1Changed(string? value)
    {
        Console.WriteLine($"Name has changed to {value}");
    }
}
