#!/bin/sh
cd ..
7z a proj1/code.7z -- \
    proj1/*.cs proj1/proj1.csproj proj1/proj1.sln proj1/bin/ proj1/obj/ \
    proj1/make-7z.sh proj1/CA2-BlackJack.pdf \
    proj1/.gitignore proj1/.git/
