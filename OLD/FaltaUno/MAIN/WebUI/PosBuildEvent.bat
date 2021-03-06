SET LPATH=%cd%
cd..
cd..
cd Build
SET RPATH=%cd%


XCOPY  "%LPATH%\AuthenticationUI\*.aspx"  "%RPATH%\"  /Y /C /E
XCOPY  "%LPATH%\AuthenticationUI\Scripts"  "%RPATH%\Scripts\"  /Y /C /E
XCOPY  "%LPATH%\AuthenticationUI\ScriptsControllers"  "%RPATH%\ScriptsControllers\"  /Y /C /E
XCOPY  "%LPATH%\AuthenticationUI\Templates"  "%RPATH%\Templates\"  /Y /C /E
XCOPY  "%LPATH%\AuthenticationUI\Styles"  "%RPATH%\Styles\"  /Y /C /E
XCOPY  "%LPATH%\AuthenticationUI\Themes"  "%RPATH%\Themes\"  /Y /C /E
XCOPY  "%LPATH%\AuthenticationUI\Controllers"  "%RPATH%\Controllers\"  /Y /C /E
XCOPY  "%LPATH%\AuthenticationUI\Assets"  "%RPATH%\Assets\" /Y  /C /E
XCOPY  "%LPATH%\AuthenticationUI\*.html"  "%RPATH%\" /Y  /C

XCOPY  "%LPATH%\DefaultUI\*.aspx"  "%RPATH%\"  /Y /C /E
XCOPY  "%LPATH%\DefaultUI\Scripts"  "%RPATH%\Scripts\"  /Y /C /E
XCOPY  "%LPATH%\DefaultUI\ScriptsControllers"  "%RPATH%\ScriptsControllers\"  /Y /C /E
XCOPY  "%LPATH%\DefaultUI\Templates"  "%RPATH%\Templates\"  /Y /C /E
XCOPY  "%LPATH%\DefaultUI\Styles"  "%RPATH%\Styles\"  /Y /C /E
XCOPY  "%LPATH%\DefaultUI\Themes"  "%RPATH%\Themes\"  /Y /C /E
XCOPY  "%LPATH%\DefaultUI\Controllers"  "%RPATH%\Controllers\"  /Y /C /E
XCOPY  "%LPATH%\DefaultUI\*.html"  "%RPATH%\" /Y  /C

XCOPY  "%LPATH%\ErrorHandlerUI\*.aspx"  "%RPATH%\"  /Y /C /E
XCOPY  "%LPATH%\ErrorHandlerUI\Scripts"  "%RPATH%\Scripts\"  /Y /C /E
XCOPY  "%LPATH%\ErrorHandlerUI\ScriptsControllers"  "%RPATH%\ScriptsControllers\"  /Y /C /E
XCOPY  "%LPATH%\ErrorHandlerUI\Templates"  "%RPATH%\Templates\"  /Y /C /E
XCOPY  "%LPATH%\ErrorHandlerUI\Styles"  "%RPATH%\Styles\"  /Y /C /E
XCOPY  "%LPATH%\ErrorHandlerUI\Themes"  "%RPATH%\Themes\"  /Y /C /E
XCOPY  "%LPATH%\ErrorHandlerUI\Controllers"  "%RPATH%\Controllers\"  /Y /C /E
XCOPY  "%LPATH%\ErrorHandlerUI\Assets"  "%RPATH%\Assets\" /Y  /C /E
XCOPY  "%LPATH%\ErrorHandlerUI\*.html"  "%RPATH%\" /Y  /C

XCOPY  "%LPATH%\MainAppUI\*.aspx"  "%RPATH%\"  /Y /C /E
XCOPY  "%LPATH%\MainAppUI\Scripts"  "%RPATH%\Scripts\"  /Y /C /E
XCOPY  "%LPATH%\MainAppUI\ScriptsControllers"  "%RPATH%\ScriptsControllers\"  /Y /C /E
XCOPY  "%LPATH%\MainAppUI\Templates"  "%RPATH%\Templates\"  /Y /C /E
XCOPY  "%LPATH%\MainAppUI\Styles"  "%RPATH%\Styles\"  /Y /C /E
XCOPY  "%LPATH%\MainAppUI\Themes"  "%RPATH%\Themes\"  /Y /C /E
XCOPY  "%LPATH%\MainAppUI\Controllers"  "%RPATH%\Controllers\"  /Y /C /E
XCOPY  "%LPATH%\MainAppUI\img"  "%RPATH%\img\"  /Y /C /E
XCOPY  "%LPATH%\MainAppUI\Assets"  "%RPATH%\Assets\" /Y  /C /E
XCOPY  "%LPATH%\MainAppUI\*.html"  "%RPATH%\" /Y  /C


XCOPY  "%LPATH%\DebugUI\*.aspx"  "%RPATH%\"  /Y /C /E
XCOPY  "%LPATH%\DebugUI\Scripts"  "%RPATH%\Scripts\"  /Y /C /E
XCOPY  "%LPATH%\DebugUI\ScriptsControllers"  "%RPATH%\ScriptsControllers\"  /Y /C /E
XCOPY  "%LPATH%\DebugUI\Templates"  "%RPATH%\Templates\"  /Y /C /E
XCOPY  "%LPATH%\DebugUI\Styles"  "%RPATH%\Styles\"  /Y /C /E
XCOPY  "%LPATH%\DebugUI\Themes"  "%RPATH%\Themes\"  /Y /C /E
XCOPY  "%LPATH%\DebugUI\Controllers"  "%RPATH%\Controllers\"  /Y /C /E
XCOPY  "%LPATH%\DebugUI\Assets"  "%RPATH%\Assets\" /Y  /C /E
XCOPY  "%LPATH%\DebugUI\*.html"  "%RPATH%\" /Y  /C

XCOPY  "%LPATH%\PlayerUI\*.aspx"  "%RPATH%\"  /Y /C /E
XCOPY  "%LPATH%\PlayerUI\Scripts"  "%RPATH%\Scripts\"  /Y /C /E
XCOPY  "%LPATH%\PlayerUI\ScriptsControllers"  "%RPATH%\ScriptsControllers\"  /Y /C /E
XCOPY  "%LPATH%\PlayerUI\Templates"  "%RPATH%\Templates\"  /Y /C /E
XCOPY  "%LPATH%\PlayerUI\Styles"  "%RPATH%\Styles\"  /Y /C /E
XCOPY  "%LPATH%\PlayerUI\Themes"  "%RPATH%\Themes\"  /Y /C /E
XCOPY  "%LPATH%\PlayerUI\Controllers"  "%RPATH%\Controllers\"  /Y /C /E
XCOPY  "%LPATH%\PlayerUI\Assets"  "%RPATH%\Assets\" /Y  /C /E
XCOPY  "%LPATH%\PlayerUI\*.html"  "%RPATH%\" /Y  /C

XCOPY  "%LPATH%\MatchUI\*.aspx"  "%RPATH%\"  /Y /C /E
XCOPY  "%LPATH%\MatchUI\Scripts"  "%RPATH%\Scripts\"  /Y /C /E
XCOPY  "%LPATH%\MatchUI\ScriptsControllers"  "%RPATH%\ScriptsControllers\"  /Y /C /E
XCOPY  "%LPATH%\MatchUI\Templates"  "%RPATH%\Templates\"  /Y /C /E
XCOPY  "%LPATH%\MatchUI\Styles"  "%RPATH%\Styles\"  /Y /C /E
XCOPY  "%LPATH%\MatchUI\Themes"  "%RPATH%\Themes\"  /Y /C /E
XCOPY  "%LPATH%\MatchUI\Controllers"  "%RPATH%\Controllers\"  /Y /C /E
XCOPY  "%LPATH%\MatchUI\Assets"  "%RPATH%\Assets\" /Y  /C /E
XCOPY  "%LPATH%\MatchUI\*.html"  "%RPATH%\" /Y  /C

XCOPY  "%LPATH%\ServerSentEventUI\*.aspx"  "%RPATH%\"  /Y /C /E
XCOPY  "%LPATH%\ServerSentEventUI\Scripts"  "%RPATH%\Scripts\"  /Y /C /E
XCOPY  "%LPATH%\ServerSentEventUI\ScriptsControllers"  "%RPATH%\ScriptsControllers\"  /Y /C /E
XCOPY  "%LPATH%\ServerSentEventUI\Templates"  "%RPATH%\Templates\"  /Y /C /E
XCOPY  "%LPATH%\ServerSentEventUI\Styles"  "%RPATH%\Styles\"  /Y /C /E
XCOPY  "%LPATH%\ServerSentEventUI\Themes"  "%RPATH%\Themes\"  /Y /C /E
XCOPY  "%LPATH%\ServerSentEventUI\Controllers"  "%RPATH%\Controllers\"  /Y /C /E
XCOPY  "%LPATH%\ServerSentEventUI\Assets"  "%RPATH%\Assets\" /Y  /C /E
XCOPY  "%LPATH%\ServerSentEventUI\*.html"  "%RPATH%\" /Y  /C


XCOPY  "%LPATH%\TeamUI\*.aspx"  "%RPATH%\"  /Y /C /E
XCOPY  "%LPATH%\TeamUI\Scripts"  "%RPATH%\Scripts\"  /Y /C /E
XCOPY  "%LPATH%\TeamUI\ScriptsControllers"  "%RPATH%\ScriptsControllers\"  /Y /C /E
XCOPY  "%LPATH%\TeamUI\Templates"  "%RPATH%\Templates\"  /Y /C /E
XCOPY  "%LPATH%\TeamUI\Styles"  "%RPATH%\Styles\"  /Y /C /E
XCOPY  "%LPATH%\TeamUI\Themes"  "%RPATH%\Themes\"  /Y /C /E
XCOPY  "%LPATH%\TeamUI\Controllers"  "%RPATH%\Controllers\"  /Y /C /E
XCOPY  "%LPATH%\TeamUI\Assets"  "%RPATH%\Assets\" /Y  /C /E
XCOPY  "%LPATH%\TeamUI\*.html"  "%RPATH%\" /Y  /C
