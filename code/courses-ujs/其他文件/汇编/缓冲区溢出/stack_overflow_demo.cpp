#include<stdio.h>
#include<windows.h>
#define Format "%s"
#define Filename "1.txt"
#define Mode "r"

int __cdecl fun(char *Source)
{
  char *Dest=new char[500]; // [esp+0h] [ebp-1F8h]

  strcpy(Dest, Source);//
  printf(Format, Dest);                        // Format="%s"
  return 0;
}


int __cdecl main(int argc, const char **argv, const char **envp)
{
  FILE *File; // ST10_4
  char *DstBuf; // [esp+4h] [ebp-500h]

  File = fopen(Filename, Mode);                 // Filename="1.txt" Mode="r"
  fread(DstBuf, 0x500u, 1u, File);
  fun(DstBuf);                                 // subfunction
  return 0;
}
