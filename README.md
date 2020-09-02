# Neobooru

Neobooru is an image board written in C# with the use of asp.net core, entity framework and razor. The goal of this project was to create an image board with a refreshed design while providing the same *(or even more)* features than the older, more traditional ones. Neobooru is a hobby project of mine and as of the moment most basic functionality is implemented however the pools, shop, help sections and the REST API are not implemented yet.

# Installation

 In order to run neobooru on linux you must install the [.net core runtime and development kit](https://docs.microsoft.com/en-us/dotnet/core/install/linux). After having installed it you have to either compile the server or download a binary release from the [releases section](https://github.com/TheSlipper/neobooru/releases). Pick your architecture and operating system and extract it to the folder of your choice. As for the database server you will need a microsoft sql server. The project was tested with the docker release `2019-latest` and it is suggested that you go with this release. To populate the database with the necessary tables you must navigate to the neobooru project in the neobooru solution *(neobooru/neobooru)* and  run those commands:

```
dotnet tool install --global dotnet-ef
dotnet ef database update
```

After that you can simply run the server by running the neobooru.exe file.

# Screenshots

