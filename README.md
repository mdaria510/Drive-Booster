# Drive-Booster
With Windows 10, Microsoft added several new file system compression algorithms, and hid them away in the compact.exe utility. Unlike NTFS compression, these algorithms are multi-threaded and adequately fast enough to use for real time applications - however there are many usability issues with their implementation that make it insuitable for "set it and forget it" usage. This program was initially conceived to solve those usability issues with a GUI, but before long I realized there was far more potential.

In a nutshell, solid state drives are still way too expensive to store a huge library of games and programs. NTFS compression is slow to decode and has a poor compression ratio. The new algorithms are much more efficient, and have a much wider range of performance/ratio. The highest ratio algorithm, LZX, can have a measurable performance hit on load times. What was needed was a way to determine which files would benefit most from higher compression, which should be only lightly touched because they are performance critical, and which should not be compressed at all, typically because they are already compressed. A lot of work went into analyzing millions of files and load patterns to come up with the ideal mix. The goal is to compress as much as possible without compromising performance at all. Initial tests have shown it is very close to that ideal.  

Results may vary significantly. Indie games tend to compress very well, but AAA games are often pre-compressed. For some reason, Oculus games compress very, very well. On my 1TB games library with a mix of games, total savings is about 10-20%. 

The architecture may appear overdesigned at first glance, but it was built to be extensible. Because there are many other technqiues that can be used to trim down the SSD footprint of games (and programs) I wanted to ensure those could eventually be supported. 

Unfortunately, I'm a not a professional developer by day (yet), and while the core algorithm works perfectly, the finer aspects of GUI programming, multithreading and queueing are a few steps above my current level of expertise. I welcome any and all contributors who can assist in refining the GUI into something far more presentable, and from there hopefully some of the more aspirational components can be implemented.    

**If you're interested in contributing, get in touch and I'll take whatever time is necessary to explain the finer points of the code to you so you can get up to speed as soon as possible.**

**If you're primarily interested in actually using the program....I hope to eventually improve it to a state where it is fit for public consumption, but for now, use at your own risk, expect bugs, and there will be no support provided.** 




#### A set of helper utilities and scripts are also available:

**Win 10 Compression Helpers** - A set of batch files to compress a set of folders with one of the new Windows 10 file system compression algorithms. Place shortcuts in "Send To" folder and use it directly with folders in explorer.

**Drive Swap** - A batch file that automates the process of shifting folders between two drives, and using hard links/junctions to link back to them. Similar functionalilty to Steam Mover, and makes swapping giant game folders between drives a piece of cake. Compression functionality is built in as well, but currently commented out. Parameters should be fairly self-explanatory.

**Tiered Storage Optimization** - A powershell script that when used in conjunction with a tiered storage space, automatically pins files to the slow tier that do not benefit from the speed of a solid state drive. This technique can save *very* large amounts of disk space when used with a folder of games. Because it uses storage spaces as a foundation, it is extremely safe to use and does not put data at risk.

**Hybridizer** - A simple C# program that can selectively move files to another drive and symbolically link back, effectively giving the user the ability to manually create tiered storage. Requires elevation, and is virtually guaranteed to break things, so *use at your own risk, preferably not at all.* Primarily a proof of concept, but perfectly usable if you're comfortable with file system manipulation and are willing to risk non-critical data.

