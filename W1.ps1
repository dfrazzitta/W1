$chromePath = "C:\Program Files\Google\Chrome\Application\chrome.exe"
$arguments = "--incognito", "http://192.168.0.6:5000"
Start-Process -FilePath $chromePath -ArgumentList $arguments
cd C:\code
dotnet run

