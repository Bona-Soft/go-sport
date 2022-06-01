@Echo off

:: ADD IIS ENTRY
SET /P SITE_NAME="Enter the name of the website (noSpace) "

%windir%\system32\inetsrv\AppCmd ADD SITE /name:"%SITE_NAME%" /bindings:http://%SITE_NAME%.127-0-0-1org.uk:80 /physicalPath:"%cd%"

echo Web site added: %SITE_NAME%
echo Binding:  http://%SITE_NAME%.127-0-0-1org.uk:80
echo ...
echo COMPLETED!
