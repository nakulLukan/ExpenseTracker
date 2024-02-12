# ExpenseTracker
An MAUI mobile application to track expense on a daily basis

## How to publish?
`dotnet publish -f net8.0-android34.0 -c Release -p:AndroidKeyStore=true -p:AndroidSigningKeyStore=../key.keystore -p:AndroidSigningKeyAlias=ExpenseTracker -p:AndroidSigningKeyPass=password -p:AndroidSigningStorePass=password`