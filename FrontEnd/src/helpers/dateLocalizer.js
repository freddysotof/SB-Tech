
export const dateLocalizer = {
    name: 'es', // name String
    weekdays: 'Domingo_Lunes_Martes_Miercoles_Jueves_Viernes_Sabado'.split('_'), // weekdays Array
    weekdaysShort: 'Dom_Lun_Mar_Mie_Jue_Vie_Sab'.split('_'), // OPTIONAL, short weekdays Array, use first three letters if not provided
    weekdaysMin: 'Do_Lu_Ma_Mi_Ju_Vi_Sa'.split('_'), // OPTIONAL, min weekdays Array, use first two letters if not provided
    weekStart: 1, // OPTIONAL, set the start of a week. If the value is 1, Monday will be the start of week instead of Sunday。
    // yearStart: 4, // OPTIONAL, the week that contains Jan 4th is the first week of the year.
    months: 'Enero_Febrero_Marzo_Abril_Mayo_Junio_Julio_Agosto_Septiembre_Octubre_Noviembre_Diciembte'.split('_'), // months Array
    monthsShort: 'Ene_Feb_Mar_Abr_May_Jun_Jul_Ago_Sep_Oct_Nov_Dic'.split('_'), // OPTIONAL, short months Array, use first three letters if not provided
    ordinal: n => `${n}º`, // ordinal Function (number) => return number + output
    formats: {
      // abbreviated format options allowing localization
      LTS: 'h:mm:ss A',
      LT: 'h:mm A',
      L: 'dd-MM-YYYY',
      LL: 'MMMM D, YYYY',
      LLL: 'MMMM D, YYYY h:mm A',
      LLLL: 'dddd, MMMM D, YYYY h:mm A',
      // lowercase/short, optional formats for localization
      l: 'D/M/YYYY',
      ll: 'D MMM, YYYY',
      lll: 'D MMM, YYYY h:mm A',
      llll: 'ddd, MMM D, YYYY h:mm A'
    },
    relativeTime: {
      // relative time format strings, keep %s %d as the same
      future: 'en %s', // e.g. in 2 hours, %s been replaced with 2hours
      past: 'hace %s',
      s: 'unos segundos',
      m: 'un minuto',
      mm: '%d minutos',
      h: 'una hora',
      hh: '%d horas', // e.g. 2 hours, %d been replaced with 2
      d: 'un dia',
      dd: '%d dias',
      M: 'un mes',
      MM: '%d meses',
      y: 'un año',
      yy: '%d años'
    },
    meridiem: (hour, minute, isLowercase) => {
      // OPTIONAL, AM/PM
      return hour > 12 ? 'PM' : 'AM'
    }
}