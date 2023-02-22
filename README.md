# Liquid-Democracy
Online voting platform for Liquid democracy 

Kill the process:
lsof -i tcp:7236 
kill -9 <PID>



# Guide til User Secrets
- Stå i mappen: /Liquid-Democracy/liquid-democracy-server/API/
- Kør kommando: cat /Users/STIG_TIL_/secret.json | dotnet user-secrets set