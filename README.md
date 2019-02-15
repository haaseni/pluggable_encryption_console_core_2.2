# pluggable_encryption_console_core_2.2
An example of using pluggable encryption algorithms in a windows console application.

This program is an example of making the cryptographic algorithm of choice pluggable. This is being done because 
cryptographic algorithms can frequently be made obsolete due to many factors most notably a security vulnerability. 

By passing in the cryptographic algorithm into the encryption and decryption methods, it will make swapping out 
cryptographic algorithms much easier for an application that uses them.
