#include <SD.h>

#define SD_PIN 10

#define FILE_NAME_FORMAT "r_000.txt"
#define NUM_START_INDEX 2

#define NEW_LINE_FORMAT "000,\n"

class Card
{
    File file;

    int file_num;
    char file_num_str[4];
    char file_name[10];

    char new_line[6];
    char new_value_str[4];

public:
    bool is_connected;

private:
    void updateFileName()
    {
        if(file_num < 1000)
        {
            sprintf(file_num_str,"%d", file_num);
            if(file_num < 10)
            {
                file_name[NUM_START_INDEX]  = '0';
                file_name[NUM_START_INDEX+1]= '0';
                file_name[NUM_START_INDEX+2]= file_num_str[0];
            }
            else if(file_num < 100)
            {
                file_name[NUM_START_INDEX]  = '0';
                file_name[NUM_START_INDEX+1]= file_num_str[0];
                file_name[NUM_START_INDEX+2]= file_num_str[1];
            }
            else
            {
                file_name[NUM_START_INDEX]  = file_num_str[0];
                file_name[NUM_START_INDEX+1]= file_num_str[1];
                file_name[NUM_START_INDEX+2]= file_num_str[2];
            }
        }
    }

public:
    void begin()
    {
        if(SD.begin(SD_PIN))
            is_connected=true;
        strcpy(file_name, FILE_NAME_FORMAT);
        strcpy(new_line, NEW_LINE_FORMAT);
    }

    void open()
    {
        if(!is_connected) return;
        
        file_num = 1;
        updateFileName();
        while(SD.exists(file_name))
        {
            file_num++;
            updateFileName();
        }
        file=SD.open(file_name,FILE_WRITE);
        file.print("[\n");
        Serial.println(file_name);
    }

    void formatAndPrint(byte value)
    {
        if(!is_connected || !file) return;

        sprintf(new_value_str,"%d", ((int)value));
        if(value < 10)
        {
            new_line[0]  = ' ';
            new_line[1]= ' ';
            new_line[2]= new_value_str[0];
        }
        else if(value < 100)
        {
            new_line[0]  = ' ';
            new_line[1]= new_value_str[0];
            new_line[2]= new_value_str[1];
        }
        else
        {
            new_line[0]  = new_value_str[1];
            new_line[1]= new_value_str[2];
            new_line[2]= new_value_str[3];
        }

        file.print(new_line);
    }

    void close()
    {
        if(!is_connected || !file) return;

        file.print("]");
        file.close();
    }
};