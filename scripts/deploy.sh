#!/usr/bin/env bash

# get latest git tag and increment it
# example input: frontend/1.0.0
# example output: frontend/1.0.1

debug=0
clean_local_tags=0

prefix=$1
# get argument from command line, if argument is not set, use default as frontend
if [ -z "$prefix" ]; then
    prefix="frontend"
fi

# add / to end
prefix=${prefix%/}


# checkout to master branch and check if success
echo "Checking out to master branch"
git checkout master
if [ $? -ne 0 ]; then
    echo "Error: checkout to master branch failed"
    exit 1
fi

# clean all local tags if clean_local_tags is set to 1
if [ $clean_local_tags -eq 1 ]; then
    echo "Cleaning local tags"
    git tag -d $(git tag -l)
    if [ $? -ne 0 ]; then
        echo "Error: cleaning local tags failed"
        exit 1
    fi
fi

# get latest update
echo "Pulling latest updates"
git pull
if [ $? -ne 0 ]; then
    echo "Error: git pull failed"
    exit 1
fi

# get latest git tags array and select only ends with postFix
tags=$(git tag -l --sort=-v:refname)
if [ $debug -eq 1 ]; then
    echo "tags: $tags"
fi

# get latest tag
# example release/1.0.2-beta
latest_tag=$(echo "$tags" | head -n 1)
echo "latest tag: $latestTag"

# split latest tag by / and get last element 
# example: 1.0.2
version=${latest_tag##*/}
if [ $debug -eq 1 ]; then
    echo "Version: $version"
fi

# increment lastVersion
# example: 1.0.2 -> 1.0.3
new_version=$(echo $version | awk -F. '{$NF = $NF + 1;} 1' | sed 's/ /./g')
if [ $debug -eq 1 ]; then
    echo "New version: $new_version"
fi

# add prefix to new version
# example: frontend/1.0.3
new_tag="$prefix/$new_version"
if [ $debug -eq 1 ]; then
    echo "New tag: $new_tag"
fi

# take user input yes or no
input=""
while [ "$input" != "y" ] && [ "$input" != "n" ]
do
    read -p "Do you want to create new tag $newTag? (y/n): " input
done

# create new tag if user input is yes
if [ "$input" = "y" ]; then
    echo "Creating new tag $newTag"
    git tag $newTag
    echo "Pushing new tag $newTag"
    git push origin $newTag
fi

# return to previous branch
echo "Checking out to previous branch"
git checkout -