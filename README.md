# ExpenseTracker
An MAUI mobile application to track expense on a daily basis

## How to publish?
`dotnet publish -f net8.0-android34.0 -c Release -p:AndroidKeyStore=true -p:AndroidSigningKeyStore=../key.keystore -p:AndroidSigningKeyAlias=ExpenseTracker -p:AndroidSigningKeyPass=password -p:AndroidSigningStorePass=password -p:AndroidPackageFormats=apk -p:ApplicationId=com.lukeentertainment.houseexpensetracker -p:ApplicationTitle="Expense Tracker"`

## How to install using adb.exe
adb install <FOLDER_PATH>\com.lukeentertainment.houseexpensetracker-Signed.apk