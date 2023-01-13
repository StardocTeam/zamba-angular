export class Imap{
    PROCESS_ID: number; 
    USER_NAME: string; 
    PROCESS_NAME: string; 
    EMAIL: string; 
    USER_ID: number; 
    USER_PASSWORD: string; 
    GENERIC_INBOX: String;

    IP_ADDRESS: string; 
    FIELD_PORT: string; 
    FIELD_PROTOCOL: string; 
    HAS_FILTERS: string; 
    FILTER_FIELD: string; 
    FILTER_VALUE: string; 
    FILTER_RECENTS: string; 
    FILTER_NOT_READS: string; 
    EXPORT_ATTACHMENTS_SEPARATELY: string;

    FOLDER_NAME: string;
    FOLDER_NAME_DEST: string;     
    ENTITY_ID: string; 
    SENT_BY: string; 
    FIELD_TO: string; 
    CC: string; 
    CCO: string; 
    SUBJECT: string; 
    FIELD_BODY: string; 
    FIELD_DATE: string; 
    Z_USER: string; 
    CODE_MAIL: string; 
    TYPE_OF_EXPORT: string; 
    AUT_INCREMENT: string;
    
    /**
     *
     */
    constructor() {
        this.PROCESS_ID = -1,
        this.USER_NAME = ''; 
        this.PROCESS_NAME = ''; 
        this.EMAIL = ''; 
        this.USER_ID = -1; 
        this.USER_PASSWORD = '';
        this.GENERIC_INBOX = '';  

        this.IP_ADDRESS = ''; 
        this.FIELD_PORT = ''; 
        this.FIELD_PROTOCOL = ''; 
        this.HAS_FILTERS = ''; 
        this.FILTER_FIELD = ''; 
        this.FILTER_VALUE = ''; 
        this.FILTER_RECENTS = ''; 
        this.FILTER_NOT_READS = '';
        this.EXPORT_ATTACHMENTS_SEPARATELY = ''; 

        this.FOLDER_NAME = '';
        this.FOLDER_NAME_DEST = '';     
        this.ENTITY_ID = ''; 
        this.SENT_BY = ''; 
        this.FIELD_TO = ''; 
        this.CC = ''; 
        this.CCO = ''; 
        this.SUBJECT = ''; 
        this.FIELD_BODY = ''; 
        this.FIELD_DATE = ''; 
        this.Z_USER = ''; 
        this.CODE_MAIL = ''; 
        this.TYPE_OF_EXPORT = ''; 
        this.AUT_INCREMENT = '';
    }
}