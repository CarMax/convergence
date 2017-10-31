#REM You will need to pass in your MsBuildPath due to a bug with Nuget and VS2017

@echo off
cls

ECHO.
ECHO Building Convergence
ECHO =======================================

nuget pack src\Convergence\Convergence.csproj^
    -build^
    -Prop Configuration=Release^
    -Exclude edge/*.*^
    -MsbuildPath %1^
    -IncludeReferencedProjects^
    -OutputDirectory artifacts^
    -Verbosity quiet

ECHO.
ECHO Building Convergence.React 
ECHO =======================================

nuget pack src\Convergence.React\Convergence.React.csproj^
    -build^
    -Prop Configuration=Release^
    -MsbuildPath %1^
    -IncludeReferencedProjects^
    -OutputDirectory artifacts^
    -Verbosity quiet

ECHO.
ECHO All done
