## TODO:
*	Add include/match wildcard support in script loader (see Greasemonkey reference) (high priority)
*	Make code more human-readable. (I originally wrote this to just work, not look good.)
*	Test installer on multiple platforms
*	Add proper error handling for scripts (log file? JS Console.log?)
*	Support for all GM_ functions (possibly by a custom userscript/dom listener)
*	Make better icon for forms and button
*	Make extension run on page refresh
*	Add support for Run-At meta-tag (Document-start fires at DocumentComplete, Document-end at DownloadComplete)
*	Remove unused references for all projects
*	Use UIAutomation to close all messageboxes after a set period of time to prevent browser becoming permanently unresponsive (sometimes happens after an error in the Scriptmonkey.Run method for me)
*	Add automatic module for scripts which have an update URL
*	Move all large strings to constants
*	Add automatic update checker for the BHO itself
*	Add support for all Greasemonkey tags (e.g. @require, @exclude)
*	Add 'get userscripts' button to navigate to a userscript directory website
*	Replace `\r\n` with Environment.NewLine everywhere
*	Reduce the number of Try statements used throughout the solution
*	Implement unit testing
*	Add custom script editor path options (currently can be edited manually in settings.json)
*	Add load order option in script manager (reorder the array of scripts)
*	Random bug fixes and general enhancements
*	Add more to README.md