# vgperson Lyrics Plugin for MusicBee (MB_vgpersonLyrics)
This is a MusicBee plugin written in VB.NET to retrieve lyrics for songs from vgperson's website.

The plugin operates as a standard lyrics provider, which means it will automatically act just like one of the included lyrics sources. I believe it should work on MusicBee 2.0 and up, however testing has only been performed with MusicBee 2.5.
I don't think there's any major issues with it, but I've been wrong about many things before. If something doesn't work or could be done better, feel free to create an issue or put in a pull request.

vgpersonLyricsLib is used to retrieve the lyrics. This is currently a closed source library.

　

### Usage notes:
Usage is simple, just place any DLLs in the MusicBee plugins directory and set up your lyrics provider priorities from the 'Tags (2)' settings page.
A visual interface is provided via the MusicBee plugin settings page. This allows you to set the interface language, what languages to display in the returned lyrics (among other things).

Multilingual support is very basic and ideally should be rewritten, but for now this isn't a major issue.
User-defined languages are currently not supported.

　

### Building:
To successfully complete a build of MB_vgpersonLyrics, copies of [ILMerge](http://research.microsoft.com/en-us/people/mbarnett/ilmerge.aspx) and [7z](http://www.7-zip.org/) will be needed in the path environment variable. Without these, the output will be suitable for testing but not for a final release.

If you do build MB_vgpersonLyrics yourself, you'll most likely want to modify the post build events to suit your system, primarily the commands which copy the plugin to "D:\Program Files (x86)\MusicBee\Plugins" (in my config).

You may also wish to set the start action (under the debug tab of the project properties) to launch your copy of MusicBee too.

　

### Legal info:
NetNerd is not affiliated in any way with vgperson.

MB_vgpersonLyrics comes with no warranty or guarantees. It probably won't break anything, but I won't promise you anything. Use is at your own risk.

　

### Licensing:
MB_vgpersonLyrics is licensed under the GPL v3.

Source code can be found at https://github.com/NetNerd/MB_vgpersonLyrics.
