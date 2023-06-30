# ThirdWebSDKDebug

This is a minimal example for a deserialization issue I am experiencing when passing a bytes[] argument to a smart contract on webGL platform.

The test itself is in Assets/TestScript.cs file and will do the following:

1. On Start prompt user to connect MetaMask wallet
2. Attempt invoke a smart contract

At the moment the test will fail due to type mismatch in unity js bridge:

ThirdwebSDK invoke error Error: expected array value (argument="data" ...
