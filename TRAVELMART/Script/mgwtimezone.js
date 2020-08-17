/// <reference name="MicrosoftAjax.js"/>


function getTimezoneName(val) {

    var mydate = new Date(val); 
    
//    var timeSummer = new Date(Date.UTC(2005, 6, 30, 0, 0, 0, 0));
//    var summerOffset = -1 * timeSummer.getTimezoneOffset();
//    var timeWinter = new Date(Date.UTC(2005, 12, 30, 0, 0, 0, 0));
//    var winterOffset = -1 * timeWinter.getTimezoneOffset();
//

    var timeSummer = new Date(Date.UTC(2005, 6, 30, 0, 0, 0, 0));
    var summerOffset = -1 * timeSummer.getTimezoneOffset();
    var timeWinter = new Date(Date.UTC(2005, 12, 30, 0, 0, 0, 0));
    var winterOffset = -1 * timeWinter.getTimezoneOffset();
    
    
    var timeZoneHiddenField;

    if (-720 == summerOffset && -720 == winterOffset) { timeZoneHiddenField = 'Dateline Standard Time'; }
    else if (-660 == summerOffset && -660 == winterOffset) { timeZoneHiddenField = 'UTC-11'; }
    else if (-660 == summerOffset && -660 == winterOffset) { timeZoneHiddenField = 'Samoa Standard Time'; }
    else if (-660 == summerOffset && -600 == winterOffset) { timeZoneHiddenField = 'Hawaiian Standard Time'; }
    else if (-570 == summerOffset && -570 == winterOffset) { timeZoneHiddenField.value = 'Pacific/Marquesas'; }
    //                else if (-540 == summerOffset && -600 == winterOffset) { timeZoneHiddenField.value = 'America/Adak'; }
    //                else if (-540 == summerOffset && -540 == winterOffset) { timeZoneHiddenField.value = 'Pacific/Gambier'; }
    else if (-480 == summerOffset && -540 == winterOffset) { timeZoneHiddenField = 'Alaskan Standard Time'; }
    //                else if (-480 == summerOffset && -480 == winterOffset) { timeZoneHiddenField = 'Pacific/Pitcairn'; }
    else if (-420 == summerOffset && -480 == winterOffset) { timeZoneHiddenField = 'Pacific Standard Time'; }
    else if (-420 == summerOffset && -420 == winterOffset) { timeZoneHiddenField = 'US Mountain Standard Time'; }
    else if (-360 == summerOffset && -420 == winterOffset) { timeZoneHiddenField = 'Mountain Standard Time'; }
    else if (-360 == summerOffset && -360 == winterOffset) { timeZoneHiddenField = 'Central America Standard Time'; }
    //                else if (-360 == summerOffset && -300 == winterOffset) { timeZoneHiddenField = 'Pacific/Easter'; }
    else if (-300 == summerOffset && -360 == winterOffset) { timeZoneHiddenField = 'Central Standard Time'; }
    else if (-300 == summerOffset && -300 == winterOffset) { timeZoneHiddenField = 'SA Pacific Standard Time'; }
    else if (-240 == summerOffset && -300 == winterOffset) { timeZoneHiddenField = 'Eastern Standard Time'; }
    else if (-270 == summerOffset && -270 == winterOffset) { timeZoneHiddenField = 'Venezuela Standard Time'; }
    else if (-240 == summerOffset && -240 == winterOffset) { timeZoneHiddenField = 'SA Western Standard Time'; }
    else if (-240 == summerOffset && -180 == winterOffset) { timeZoneHiddenField = 'Central Brazilian Standard Time'; }
    else if (-180 == summerOffset && -240 == winterOffset) { timeZoneHiddenField = 'Atlantic Standard Time'; }
    else if (-180 == summerOffset && -180 == winterOffset) { timeZoneHiddenField = 'Montevideo Standard Time'; }
    else if (-180 == summerOffset && -120 == winterOffset) { timeZoneHiddenField = 'E. South America Standard Time'; }
    else if (-150 == summerOffset && -210 == winterOffset) { timeZoneHiddenField = 'Mid-Atlantic Standard Time'; }
    else if (-120 == summerOffset && -180 == winterOffset) { timeZoneHiddenField = 'America/Godthab'; }
    else if (-120 == summerOffset && -120 == winterOffset) { timeZoneHiddenField = 'SA Eastern Standard Time'; }
    else if (-60 == summerOffset && -60 == winterOffset) { timeZoneHiddenField = 'Cape Verde Standard Time'; }
    else if (0 == summerOffset && -60 == winterOffset) { timeZoneHiddenField = 'Azores Daylight Time'; }
    else if (0 == summerOffset && 0 == winterOffset) { timeZoneHiddenField = 'Morocco Standard Time'; }
    else if (60 == summerOffset && 0 == winterOffset) { timeZoneHiddenField = 'GMT Standard Time'; }
    else if (60 == summerOffset && 60 == winterOffset) { timeZoneHiddenField = 'Africa/Algiers'; }
    else if (60 == summerOffset && 120 == winterOffset) { timeZoneHiddenField = 'Namibia Standard Time'; }
    else if (120 == summerOffset && 60 == winterOffset) { timeZoneHiddenField = 'Central European Standard Time'; }
    else if (120 == summerOffset && 120 == winterOffset) { timeZoneHiddenField = 'South Africa Standard Time'; }
    else if (180 == summerOffset && 120 == winterOffset) { timeZoneHiddenField = 'GTB Standard Time'; }
    else if (180 == summerOffset && 180 == winterOffset) { timeZoneHiddenField = 'E. Africa Standard Time'; }
    else if (240 == summerOffset && 180 == winterOffset) { timeZoneHiddenField = 'Russian Standard Time'; }
    else if (240 == summerOffset && 240 == winterOffset) { timeZoneHiddenField = 'Arabian Standard Time'; }
    else if (270 == summerOffset && 210 == winterOffset) { timeZoneHiddenField = 'Iran Standard Time'; }
    else if (270 == summerOffset && 270 == winterOffset) { timeZoneHiddenField = 'Afghanistan Standard Time'; }
    else if (300 == summerOffset && 240 == winterOffset) { timeZoneHiddenField = 'Pakistan Standard Time'; }
    else if (300 == summerOffset && 300 == winterOffset) { timeZoneHiddenField = 'West Asia Standard Time'; }
    else if (330 == summerOffset && 330 == winterOffset) { timeZoneHiddenField = 'India Standard Time'; }
    else if (345 == summerOffset && 345 == winterOffset) { timeZoneHiddenField = 'Nepal Standard Time'; }
    else if (360 == summerOffset && 300 == winterOffset) { timeZoneHiddenField = 'N. Central Asia Standard Time'; }
    else if (360 == summerOffset && 360 == winterOffset) { timeZoneHiddenField = 'Central Asia Standard Time'; }
    else if (390 == summerOffset && 390 == winterOffset) { timeZoneHiddenField = 'Myanmar Standard Time'; }
    else if (420 == summerOffset && 360 == winterOffset) { timeZoneHiddenField = 'North Asia Standard Time'; }
    else if (420 == summerOffset && 420 == winterOffset) { timeZoneHiddenField = 'SE Asia Standard Time'; }
    else if (480 == summerOffset && 420 == winterOffset) { timeZoneHiddenField = 'North Asia East Standard Time'; }
    else if (480 == summerOffset && 480 == winterOffset) { timeZoneHiddenField = 'China Standard Time'; }
    else if (540 == summerOffset && 480 == winterOffset) { timeZoneHiddenField = 'Yakutsk Standard Time'; }
    else if (540 == summerOffset && 540 == winterOffset) { timeZoneHiddenField = 'Tokyo Standard Time'; }
    else if (570 == summerOffset && 570 == winterOffset) { timeZoneHiddenField = 'Cen. Australia Standard Time'; }
    else if (570 == summerOffset && 630 == winterOffset) { timeZoneHiddenField = 'Australia/Adelaide'; }
    else if (600 == summerOffset && 540 == winterOffset) { timeZoneHiddenField = 'Asia/Yakutsk'; }
    else if (600 == summerOffset && 600 == winterOffset) { timeZoneHiddenField = 'E. Australia Standard Time'; }
    else if (600 == summerOffset && 660 == winterOffset) { timeZoneHiddenField = 'AUS Eastern Standard Time'; }
    else if (630 == summerOffset && 660 == winterOffset) { timeZoneHiddenField = 'Australia/Lord_Howe'; }
    else if (660 == summerOffset && 600 == winterOffset) { timeZoneHiddenField = 'Tasmania Standard Time'; }
    else if (660 == summerOffset && 660 == winterOffset) { timeZoneHiddenField = 'West Pacific Standard Time'; }
    else if (690 == summerOffset && 690 == winterOffset) { timeZoneHiddenField = 'Central Pacific Standard Time'; }
    else if (720 == summerOffset && 660 == winterOffset) { timeZoneHiddenField = 'Magadan Standard Time'; }
    else if (720 == summerOffset && 720 == winterOffset) { timeZoneHiddenField = 'Fiji Standard Time'; }
    else if (720 == summerOffset && 780 == winterOffset) { timeZoneHiddenField = 'New Zealand Standard Time'; }
    else if (765 == summerOffset && 825 == winterOffset) { timeZoneHiddenField = 'Pacific/Chatham'; }
    else if (780 == summerOffset && 780 == winterOffset) { timeZoneHiddenField = 'Tonga Standard Time'; }
    else if (840 == summerOffset && 840 == winterOffset) { timeZoneHiddenField = 'Pacific/Kiritimati'; }
    else { timeZoneHiddenField = 'US/Pacific'; }
    return timeZoneHiddenField;

}


function GetTimezoneID() {


    var objdatetime = new Date();
   
    var timezone = objdatetime.toTimeString();
    var tzstr = timezone.split("(");
    return tzstr[1].toString().replace(")", "");

}

function GetTimezoneName() {


    var d = new Date();
    var usertime = d.toLocaleString();

    // Some browsers / OSs provide the timezone name in their local string:
    var tzsregex = /\b(ACDT|ACST|ACT|ADT|AEDT|AEST|AFT|AKDT|AKST|AMST|AMT|ART|AST|AWDT|AWST|AZOST|AZT|BDT|BIOT|BIT|BOT|BRT|BST|BTT|CAT|CCT|CDT|CEDT|CEST|CET|CHADT|CHAST|CIST|CKT|CLST|CLT|COST|COT|CST|CT|CVT|CXT|CHST|DFT|EAST|EAT|ECT|EDT|EEDT|EEST|EET|EST|FJT|FKST|FKT|GALT|GET|GFT|GILT|GIT|GMT|GST|GYT|HADT|HAEC|HAST|HKT|HMT|HST|ICT|IDT|IRKT|IRST|IST|JST|KRAT|KST|LHST|LINT|MART|MAGT|MDT|MET|MEST|MIT|MSD|MSK|MST|MUT|MYT|NDT|NFT|NPT|NST|NT|NZDT|NZST|OMST|PDT|PETT|PHOT|PKT|PST|RET|SAMT|SAST|SBT|SCT|SGT|SLT|SST|TAHT|THA|UYST|UYT|VET|VLAT|WAT|WEDT|WEST|WET|WST|YAKT|YEKT)\b/gi;

    // In other browsers the timezone needs to be estimated based on the offset:
    var timezonenames = { "UTC+0": "GMT", "UTC+1": "CET", "UTC+2": "EET", "UTC+3": "EEDT", "UTC+3.5": "IRST", "UTC+4": "MSD", "UTC+4.5": "AFT", "UTC+5": "PKT", "UTC+5.5": "IST", "UTC+6": "BST", "UTC+6.5": "MST", "UTC+7": "THA", "UTC+8": "AWST", "UTC+9": "AWDT", "UTC+9.5": "ACST", "UTC+10": "AEST", "UTC+10.5": "ACDT", "UTC+11": "AEDT", "UTC+11.5": "NFT", "UTC+12": "NZST", "UTC-1": "AZOST", "UTC-2": "GST", "UTC-3": "BRT", "UTC-3.5": "NST", "UTC-4": "CLT", "UTC-4.5": "VET", "UTC-5": "EST", "UTC-6": "CST", "UTC-7": "MST", "UTC-8": "PST", "UTC-9": "AKST", "UTC-9.5": "MIT", "UTC-10": "HST", "UTC-11": "SST", "UTC-12": "BIT" };

    var timezone = usertime.match(tzsregex);
    if (timezone) {
        timezone = timezone[timezone.length - 1];
    } else {
        var offset = -1 * d.getTimezoneOffset() / 60;
        offset = "UTC" + (offset >= 0 ? "+" + offset : offset);
        timezone = timezonenames[offset];
    }
    
    
    
    return timezone;

}


/* http://upload.wikimedia.org/wikipedia/commons/0/01/2007-02-20_time_zones.svg */
/* 
In the northern emisphere, january is in winter and july is in summer.
In the southern emisphere, january is in summer and july is in winter.
A daylight saving time offset is always -1, so standard time is always 
the greatest of two offsets.
*/
(function() {
    var jsk = {
        /******* PROPERTIES *******/
        breakingMonth: 0,
        testMonth0Offset: null,
        testMonth6Offset: null,
        /******* PRIVATE *******/
        getDateOffset: function getDate(month) {
            return new Date((new Date()).getFullYear(), month, 0).getTimezoneOffset();
        },
        getMonth0Offset: function() {
            return jsk.testMonth0Offset != null ?
        jsk.testMonth0Offset :
        jsk.getDateOffset(jsk.breakingMonth);
        },
        getMonth6Offset: function() {
            return jsk.testMonth6Offset != null ?
        jsk.testMonth6Offset :
        jsk.getDateOffset(jsk.breakingMonth + 6);
        },
        offsetToString: function(offset) {
            var st = offset / 60.0;
            sign = st >= 0 ? "+" : "-";

            var hour = Math.floor(Math.abs(st));
            hour = (hour <= 9 ? "0" : "") + hour;

            var min = Math.abs(st % 1.0) * 60;
            min = (min <= 9 ? "0" : "") + min;

            return sign + hour + min;
        },
        /******* PUBLIC *******/
        // Force some test offsets
        testOffset: function testOffset(month0Offset, month6Offset) {
            jsk.testMonth0Offset = month0Offset;
            jsk.testMonth6Offset = month6Offset;
        },
        // the timezone has daylight saving
        hasDst: function() {
            return jsk.st() != jsk.dst();
        },
        // Returns the standard time offset (inverted)
        invertedSt: function invertedSt() {
            return Math.max(jsk.getMonth0Offset(), jsk.getMonth6Offset());
        },
        // Returns the daylight saving time offset (inverted)
        invertedDst: function invertedDst() {
            return Math.min(jsk.getMonth0Offset(), jsk.getMonth6Offset());
        },
        // Returns the standard time offset
        st: function st() {
            return 0 - jsk.invertedSt();
        },
        // Returns standard to string
        stToString: function() {
            return jsk.offsetToString(jsk.st());
        },
        // Returns the standard time offset
        dst: function dst() {
            return 0 - jsk.invertedDst();
        },
        // Returns daylight saving to string
        dstToString: function() {
            return jsk.offsetToString(jsk.dst());
        },
        iHateTheLastComma: true // this line exists because I hate the last comma
    }

    // Creates the base namespaces
    if (typeof (window["javascriptKataDotCom"]) == "undefined")
        window.javascriptKataDotCom = {};
    if (typeof (window["jsKata"]) == "undefined")
        window.jsKata = window.javascriptKataDotCom;
    if (typeof (window["jsk"]) == "undefined")
        window.jsk = window.javascriptKataDotCom;
    if (typeof (window["_"]) == "undefined")
        window._ = window.javascriptKataDotCom;

    window.javascriptKataDotCom.timezone = jsk;
    window.javascriptKataDotCom.tz = jsk;
})()