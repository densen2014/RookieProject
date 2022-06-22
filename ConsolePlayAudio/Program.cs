// See https://aka.ms/new-console-template for more information
using NAudio.Wave;

Console.WriteLine("Hello, World!");

//本地文件

//var audioFile1 = @"F:\MUSIC\test\test\林俊杰-女儿情.mp3";
//using (var audioFile = new AudioFileReader(audioFile1))
//using (var outputDevice = new WaveOutEvent())
//{
//    outputDevice.Init(audioFile);
//    outputDevice.Play();
//    while (outputDevice.PlaybackState == PlaybackState.Playing)
//    {
//        Thread.Sleep(1000);
//    }
//}


//网络文件
var url = "https://apies.blazor.zone/hello_word.mp3";
using (var mf = new MediaFoundationReader(url))
using (var wo = new WasapiOut())
{
    wo.Init(mf);
    wo.Play();
    while (wo.PlaybackState == PlaybackState.Playing)
    {
        Thread.Sleep(1000);
    }
}