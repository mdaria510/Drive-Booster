# Drive-Booster
Optimized selection of file system compression algorithm based on file contents to minimize storage usage while maximizing performance.

## Project is currently undergoing reorganization.

#### A set of helper utilities and scripts are still available.

**Win 10 Compression Helpers** - A set of batch files to compress a set of folders with one of the new Windows 10 file system compression algorithms. Place shortcuts in "Send To" folder and use it directly with folders in explorer.

**Tiered Storage Optimization** - A powershell script that when used in conjunction with a tiered storage space, automatically pins files to the slow tier that do not benefit from the speed of a solid state drive. This technique can save *very* large amounts of disk space when used with a folder of games. 

**HDD Swap** - A batch file that automates the process of shifting folders between two drives, and using hard links/junctions to link back to them. Similar functionalilty to Steam Mover, and makes swapping giant game folders between drives a piece of cake. Compression functionality is built in as well, but currently commented out.  