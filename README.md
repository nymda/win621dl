# win621dl

win621dl - windows alternative to the standard python e621 downloader.

Updated for the new e621 API.

Since last update:  
Added back in support for inkbunny  
Fixed duplicate downloads (skips existing files)  
Each downloader exists in its own class now  

Possible issues:  
API returns 300 items per request, requests are sent recursively. Due to this, downloading a lot of items may result in a stack overflow.  
E621 sometimes returns "Null" in place of some bool values, if this happens unexpectedly it can cause a crash   

Plans:  
Web interface for viewing downloaded files  
