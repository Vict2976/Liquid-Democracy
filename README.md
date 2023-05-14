# Liquid-Democracy
Online voting platform based on MitID 

Kill the process:
lsof -i tcp:7236 
kill -9 <PID>



# Guide til User Secrets
- Stå i mappen: /Liquid-Democracy/liquid-democracy-server/API/
- Kør kommando: cat /Users/STIG_TIL_/secret.json | dotnet user-secrets set



# Åbeen scuffed browser:
- open -na Google\ Chrome --args --user-data-dir=/tmp/temporary-chrome-profile-dir --disable-web-security
