# Simple Password Salting
Simple Password Salting using Sha256.

## Why this project
People normally says "you do not need to reinvent the wheel" if it's already existing. True enough, but nobody says you cannot enhance nor improve the wheel, and this is the reason why this project was created. The main purpose for now is to establish a concrete foundation of another bigger project. :)

For now, you can use the codes anytime and update it at your own expense.

## What it Does
- Generates and Retrieves Salts for each enrolled user
- Generates JSON file, if not yet existing, to your prefered path
- Generate hashed password.

## Usage
1. Add the dll to your project reference. You can download the compiled dll or compile the psalt project and add to your VS solution
2. Add psalt to Usings
3. Initialize Psalt. e.g.

```
using psalt;
//.... other codes ...
public void AnyFunction(string username, string password)
{
  var saltPass = new Psalt(<MySaltFilePath>); 
  //Path is optional. Salt file will be saved to the application's folder if not defined.
  var encryptedPass = saltPass.EncryptPassword(username, password); // new or get existing
  var updatePass = saltPass.EncryptPassword(username, password, true); // update existing
}
```
## Decryption
It is my intention not to add decryp function, unless anybody can figure out how to. A smoothie cannot be brought back as fruit.

## TODO
This project started as a simple password encryption, but I believe this can be expanded to a more decent solution online/offline for web and desktop development. Currently, what I am missing here and probably make it more usable are the following:
- File encryption
- Collection management, i.e. Remove unnecessary entries, etc.
- Nuget Package
- Possible LINQ extension (?)

## FUTURE TODO
hmm.... this will be on another project
