IP地址作为一个byte数组，转换为IP对应的数字，IP地址每位都是用一个byte来存储的，合计4个byte，也就是一个int的字节数，但是转换时需要注意，c#把最高位作为了符号位，二进制位移到符号位时会导致溢出变为负数，int最大表示值也只是31个1组成的2147483647。如果需要转换，请转换为长整型long再直接位移。

```c#
long number = (long) bytes[0] << 24 | (long) bytes[1] << 16 | (long) bytes[2] << 8 | (long) bytes[3];
```

