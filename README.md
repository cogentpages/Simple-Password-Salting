# Simple-Password-Salting
Simple Password Salting using Sha256.

## Usage:
1. Add the dll to your project reference.
2. Add psalt to Usings
2. Initialize Psalt. e.g.
```
using psalt;
.... other codes ...
public void AnyFunction(string username, string password)
{
  var saltPass = new Psalt();
  var encryptedPass = saltPass.EncryptPassword(username, password);
}
```
