
If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{   
#"No Administrative rights, it will display a popup window asking user for Admin rights"

$arguments = "& '" + $myinvocation.mycommand.definition + "'"
Start-Process "$psHome\powershell.exe" -Verb runAs -ArgumentList $arguments

break
}
#"After user clicked Yes on the popup, your file will be reopened with Admin rights"
#"Put your code here"


$SSDTier = "Games_4ColSSDSimple";
$HDDTier = "Games_2ColHDDSimple";

$_TypesToPinToHDD = ".7z",".cab",".rar",".msi",".avi",".bik",".bk2",".flv",".m4v",".mp4",".mpg",".mov",".wmv",".vp6",".swf",".mkv",".ogv",".usm",".sfd",".webm",".flv",".ivf",".asr",".aac",".wav",".mp3",".wma",".bak",".ogg",".dmp",".mdmp",".tmp",".jpg",".gif",".png",".chm",".doc",".txt",".docx",".htm",".pdf",".gz",".jpeg",".svg",".wem",".mp3",".flac",".sabs",".sabl",".pck",".m4b",".m4a",".sfd",".mpg",".tfc",".mega2",".pages";
$_NamesToPinToHDD = "readme",,"debug","bonus","manual","codec","screenshot","license","video","cinema","film","movie","music","soundtrack","song","theme","story","cutscene","speech","dialog","voice","localiz","backup","italian","french","spanish","mexican","polish","chinese","japanese","english","portugese","brazilian","german","korean","russian","czech","greek","turkish";
$DirTypesToPinToHDD = "debug","redis","dist","install","bonus","manual","support","setup","readme","driver","codec","prereq","export","extra","dotnetfx","eula","screenshot","license", "video","cinema","film","movie","music","soundtrack","song","theme","story","cutscene","speech","dialog","voice","localiz","backup","italian","french","spanish","mexican","polish","chinese","japanese","english","portugese","brazilian","german","korean","russian","czech","greek","turkish";

$matchcount = 1;
$i = 1;
[long]$sizePinned = 1;


Get-ChildItem -Path G:\ -Recurse | 

foreach {
    $match = $false;
    $i++; 
   

    Write-Progress -Activity “Pinning Files” -status "Matched $matchcount files.    $([math]::Round($sizePinned / 1GB, 1)) GB Pinned to HDD. Current Folder: $($_.Directory)" 
    #-percentComplete ($i / $numOfFolders.count*100) 
    #Current Folder: $($_.Directory)

    if ($_.extension -in $_TypesToPinToHDD) {
        
        #write-host -NoNewline "Ext " 
        #write-host $_.FullName
        $matchcount++;
        $sizePinned += $_.Length;       
        set-filestoragetier -DesiredStorageTierFriendlyName $HDDTier -Filepath $_.FullName -ea silentlycontinue;
        

        #continue;      
    } 
    
    foreach ($name in $_NamesToPinToHDD) {

            if ($_.Name -match $name) {
                $matchcount++;
                $sizePinned += $_.Length;  
                #write-host -NoNewline "Name "
                #write-host $_.FullName                
                set-filestoragetier -DesiredStorageTierFriendlyName $HDDTier -Filepath $_.FullName -ea silentlycontinue;
                $match = $true   
                break;   
            } 
        
    }

    if ($match -eq $false) {
  
        foreach ($dirtype in $DirTypesToPinToHDD) {

            if ($_.Directory -match $dirtype) {
                $matchcount++;
                $sizePinned += $_.Length;  
                #write-host -NoNewline "Dir "
                #write-host $_.FullName                
                set-filestoragetier -DesiredStorageTierFriendlyName $HDDTier -Filepath $_.FullName -ea silentlycontinue;      
            } 
        }
    }

} 


Defrag G: /G /V

write-host $([math]::Round($sizePinned / 1GB, 1)) GB in $matchcount files pinned to HDD.

pause