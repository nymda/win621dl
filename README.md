# win621dl

win621dl - windows alternative to the standard python e621 downloader.

Updated for the new e621 API.

Since last update:  
Now uses the newtonsoft Json handler  
Posts are now stored internally as their own class  
Displays currently downloading item (name, score, desc, etc)  

Possible issues:  
API returns 300 items per request, requests are sent recursively. Due to this, downloading a lot of items may result in a stack overflow.  
E621 sometimes returns "Null" in place of some bool values, if this happens unexpectedly it can cause a crash   

Plans:  
Add inkbunny downloader back  
Web interface for viewing downloaded files  
Avoid downloading duplicate items to save bandwidth (Currently overwrites)  
