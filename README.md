# rss_graber

### dependencies
* dotnet core 2

## Get started
You need init db (msqlocaldb default)
```
$ cd cli
$ dotnet ef database update
```

## CLI
There'are two command


command  | description
------------- | -------------
add  | add new rss chanel to graber
update  | look up all chanels and add all new items of rss 


### Commands example

```
$ dotnet run add http://habrahabr.com/rss/
```


```
$ dotnet run update
was added N
was checked X
```
N - count new rss items has been saved in db

X - amount checked items from all chanels

## WEB
### Web version for view our items

Server rendering works awesome

Client well
