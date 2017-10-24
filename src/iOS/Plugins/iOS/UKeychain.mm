#import "UKeychain.h"
#import "SAMKeychain.h"
#import "SAMKeychainQuery.h"


NSString* CreateNSString(const char* string)
{
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return [NSString stringWithUTF8String: ""];
}

char* MakeString(const NSString *string)
{
    const char* str = [string UTF8String];

	if (str == NULL)
		return NULL;
	
	char* res = (char *)malloc(strlen(str) + 1);
	strcpy(res, str);
	return res;
}

unsigned long MakeAccountArray(NSArray *accounts, UKeychainAccount **arr)
{   
    unsigned long count = [accounts count];
    *arr = (UKeychainAccount *)malloc(count * sizeof(UKeychainAccount));

    for (int i = 0; i < count; i++)
    {
        NSDictionary<NSString *, id> *account = [accounts objectAtIndex:i];

        (*arr)[i].Account = MakeString([account objectForKey:kSAMKeychainAccountKey]);
        (*arr)[i].Service = MakeString([account objectForKey:kSAMKeychainWhereKey]);
        (*arr)[i].CreatedAt = MakeString([[account objectForKey:kSAMKeychainCreatedAtKey] description]);
        (*arr)[i].LastModified = MakeString([[account objectForKey:kSAMKeychainLastModifiedKey] description]);
    }

    return count;
}

void FreeAccountArray(UKeychainAccount *arr, int count)
{
    for (int i = 0; i < count; i++)
    {
        free(arr[i].Account);
        free(arr[i].Service);
        free(arr[i].CreatedAt);
        free(arr[i].LastModified);
    }

    free(arr);
}

extern "C"
{
    //+ (NSArray *)allAccounts;
    unsigned long _UKeychain_AllAccounts(UKeychainAccount **arr)
    {
        return MakeAccountArray([SAMKeychain allAccounts], arr);
    }

    //+ (NSArray *)accountsForService:(NSString *)serviceName;
    unsigned long _UKeychain_AccountsForService(const char *serviceName, UKeychainAccount **arr)
    {
        return MakeAccountArray([SAMKeychain accountsForService:CreateNSString(serviceName)], arr);
    }

    //+ (NSString *)passwordForService:(NSString *)serviceName account:(NSString *)account;
    const char *_UKeychain_PasswordForService(const char *serviceName, const char *account)
    {
		return MakeString([SAMKeychain passwordForService:CreateNSString(serviceName) account:CreateNSString(account)]);
    }

    //+ (BOOL)deletePasswordForService:(NSString *)serviceName account:(NSString *)account;
    int _UKeychain_DeletePasswordForService(const char *serviceName, const char *account)
    {
		return [SAMKeychain deletePasswordForService:CreateNSString(serviceName) account:CreateNSString(account)] == YES ? 1 : 0;
    }

    //+ (BOOL)setPassword:(NSString *)password forService:(NSString *)serviceName account:(NSString *)account;
    int _UKeychain_SetPassword(const char *password, const char *serviceName, const char *account)
    {
		return [SAMKeychain setPassword:CreateNSString(password) forService:CreateNSString(serviceName) account:CreateNSString(account)] == YES ? 1 : 0;
    }

    void _UKeychain_FreeAccountArray(UKeychainAccount *arr, int count)
    {
        FreeAccountArray(arr, count);
    }
}