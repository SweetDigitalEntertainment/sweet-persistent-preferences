#import "SAMKeychain.h"
#import "SAMKeychainQuery.h"

typedef struct
{
    char *Account;
    char *Service;
    char *CreatedAt;
    char *LastModified;
} UKeychainAccount;