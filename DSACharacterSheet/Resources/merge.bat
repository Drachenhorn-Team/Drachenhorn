dir


rem code as Template

rem @echo off
rem ECHO SOURCE BRANCH IS %BUILD_SOURCEBRANCH%
rem IF %BUILD_SOURCEBRANCH% == refs/heads/master (
rem   ECHO Building master branch so no merge is needed.
rem   EXIT
rem )
rem SET sourceBranch=origin/%BUILD_SOURCEBRANCH:refs/heads/=%
rem ECHO GIT CHECKOUT MASTER
rem git checkout master
rem ECHO GIT STATUS
rem git status
rem ECHO GIT MERGE
rem git merge %sourceBranch% -m "Merge to master"
rem ECHO GIT STATUS
rem git status
rem ECHO GIT PUSH
rem git push origin
rem ECHO GIT STATUS
rem git status
