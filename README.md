# FlexPlus.DataExchange

This repository contains the source code of the reference client for interacting with the FlexPlus DataExchange API through the command line interface.

With this client you can download files from the API and upload files to it through the command line.

## Getting Started

To use the client, you just do the following steps:

1. Open a Powershell window.
2. Clone this repository to your local machine with:

```powershell
git clone https://github.com/world-direct/FlexPlus.DataExchange.git
```

3. Navigate to the folder of the project on your local machine with:

```powershell
cd FlexPlus.DataExchange
```

4. Execute following command:

```powershell
dotnet build
```

5. To find the created executable, navigate to folder `bin\Debug\netcoreapp3.1` in the project folder.

```powershell
cd FlexPlus.DataExchange\bin\Debug\netcoreapp3.1
```

## Using the client

### Download

To download a file you just use the `download` command with the required parameters. The downloaded CSV file will be saved in the same folder as the executable.

Windows:

```powershell
.\FlexPlus.DataExchangeApiClient.exe download -p <Process name> -e <url of API> -t <token to authenticate>
```

### Upload

To upload a file from the API just use the `upload` command with the required parameters.

Windows:

```powershell
.\FlexPlus.DataExchangeApiClient.exe upload -p <Process name> -e <url of API> -t <token to authenticate> -f <absolute or relative path of the file>
```

## Dependencies

- .NET Core 3.1
- CommandLineParser 2.8.0

## Further ressources

To get more information within the program use the `help command` with:

Windows:

```powershell
.\FlexPlus.DataExchangeApiClient.exe --help
```

If you need help to a specific command use:

Windows:

```powershell
.\FlexPlus.DataExchangeApiClient.exe upload --help
```

or

```powershell
.\FlexPlus.DataExchangeApiClient.exe download --help
```
