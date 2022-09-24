#ifndef ENUMS
#define ENUMS

enum SerialHeaderEnum
{
    PING = 0,
    ACK = 1,
    HELLO = 2,
    READING = 3,
    SCAN = 4,
    MODE = 5,
    NONE = 255
};

enum ModeEnum
{
    idle = 0,
    read = 1,
    scan = 2
};

enum StateEnum
{
    s_idle,
    s_open_sd,
    s_read,
    s_close_sd,
    s_scan
};

#endif