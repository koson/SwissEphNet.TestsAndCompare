using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SwissEphNetTests.Providers
{

    /// <summary>
    /// Sweph Dll interop
    /// </summary>
    public static class SwephDll
    {
        /// <summary>
        /// DLL Name
        /// </summary>
        const String SwephDllName = "swedll-2.06.dll";

        /// <summary>
        /// Preload the DLL
        /// </summary>
        static SwephDll()
        {
            // Check the size of the pointer
            Platform = IntPtr.Size == 8 ? "x64" : "x86";
            // Build the full library file name
            String libraryFile = Path.Combine(Path.GetDirectoryName(typeof(SwephDll).Assembly.Location), "libraries", Platform, SwephDllName);
            // Load the library
            var res = LoadLibrary(libraryFile);
            if (res == IntPtr.Zero)
                throw new InvalidOperationException("Failed to load the library.");
        }

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = false)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        /// <summary>
        /// Loaded platform
        /// </summary>
        public static string Platform { get; private set; }

        #region Constants
        /***********************************************************
         * definitions for use also by non-C programmers
         ***********************************************************/

        /* values for gregflag in swe_julday() and swe_revjul() */
        public const int SE_JUL_CAL = 0;
        public const int SE_GREG_CAL = 1;

        /*
         * planet numbers for the ipl parameter in swe_calc()
         */
        public const int SE_ECL_NUT = -1;

        public const int SE_SUN = 0;
        public const int SE_MOON = 1;
        public const int SE_MERCURY = 2;
        public const int SE_VENUS = 3;
        public const int SE_MARS = 4;
        public const int SE_JUPITER = 5;
        public const int SE_SATURN = 6;
        public const int SE_URANUS = 7;
        public const int SE_NEPTUNE = 8;
        public const int SE_PLUTO = 9;
        public const int SE_MEAN_NODE = 10;
        public const int SE_TRUE_NODE = 11;
        public const int SE_MEAN_APOG = 12;
        public const int SE_OSCU_APOG = 13;
        public const int SE_EARTH = 14;
        public const int SE_CHIRON = 15;
        public const int SE_PHOLUS = 16;
        public const int SE_CERES = 17;
        public const int SE_PALLAS = 18;
        public const int SE_JUNO = 19;
        public const int SE_VESTA = 20;
        public const int SE_INTP_APOG = 21;
        public const int SE_INTP_PERG = 22;

        public const int SE_NPLANETS = 23;

        public const int SE_AST_OFFSET = 10000;
        public const int SE_VARUNA = (SE_AST_OFFSET + 20000);

        public const int SE_FICT_OFFSET = 40;
        public const int SE_FICT_OFFSET_1 = 39;
        public const int SE_FICT_MAX = 999;
        public const int SE_NFICT_ELEM = 15;

        public const int SE_COMET_OFFSET = 1000;

        public const int SE_NALL_NAT_POINTS = (SE_NPLANETS + SE_NFICT_ELEM);

        /* Hamburger or Uranian "planets" */
        public const int SE_CUPIDO = 40;
        public const int SE_HADES = 41;
        public const int SE_ZEUS = 42;
        public const int SE_KRONOS = 43;
        public const int SE_APOLLON = 44;
        public const int SE_ADMETOS = 45;
        public const int SE_VULKANUS = 46;
        public const int SE_POSEIDON = 47;
        /* other fictitious bodies */
        public const int SE_ISIS = 48;
        public const int SE_NIBIRU = 49;
        public const int SE_HARRINGTON = 50;
        public const int SE_NEPTUNE_LEVERRIER = 51;
        public const int SE_NEPTUNE_ADAMS = 52;
        public const int SE_PLUTO_LOWELL = 53;
        public const int SE_PLUTO_PICKERING = 54;
        public const int SE_VULCAN = 55;
        public const int SE_WHITE_MOON = 56;
        public const int SE_PROSERPINA = 57;
        public const int SE_WALDEMATH = 58;

        public const int SE_FIXSTAR = -10;

        public const int SE_ASC = 0;
        public const int SE_MC = 1;
        public const int SE_ARMC = 2;
        public const int SE_VERTEX = 3;
        public const int SE_EQUASC = 4; /* "equatorial ascendant" */
        public const int SE_COASC1 = 5; /* "co-ascendant" (W. Koch) */
        public const int SE_COASC2 = 6; /* "co-ascendant" (M. Munkasey) */
        public const int SE_POLASC = 7; /* "polar ascendant" (M. Munkasey) */
        public const int SE_NASCMC = 8;

        /*
         * flag bits for parameter iflag in function swe_calc()
         * The flag bits are defined in such a way that iflag = 0 delivers what one
         * usually wants:
         *    - the default ephemeris (SWISS EPHEMERIS) is used,
         *    - apparent geocentric positions referring to the true equinox of date
         *      are returned.
         * If not only coordinates, but also speed values are required, use 
         * flag = SEFLG_SPEED.
         *
         * The 'L' behind the number indicates that 32-bit integers (Long) are used.
         */
        public const int SEFLG_JPLEPH = 1;       /* use JPL ephemeris */
        public const int SEFLG_SWIEPH = 2;       /* use SWISSEPH ephemeris */
        public const int SEFLG_MOSEPH = 4;       /* use Moshier ephemeris */

        public const int SEFLG_HELCTR = 8;     /* heliocentric position */
        public const int SEFLG_TRUEPOS = 16;     /* true/geometric position, not apparent position */
        public const int SEFLG_J2000 = 32;     /* no precession, i.e. give J2000 equinox */
        public const int SEFLG_NONUT = 64;     /* no nutation, i.e. mean equinox of date */
        public const int SEFLG_SPEED3 = 128;    /* speed from 3 positions (do not use it,
                                * SEFLG_SPEED is faster and more precise.) */
        public const int SEFLG_SPEED = 256;    /* high precision speed  */
        public const int SEFLG_NOGDEFL = 512;    /* turn off gravitational deflection */
        public const int SEFLG_NOABERR = 1024;   /* turn off 'annual' aberration of light */
        public const int SEFLG_ASTROMETRIC = (SEFLG_NOABERR | SEFLG_NOGDEFL); /* astrometric position,
                                * i.e.with light-time, but without aberration and
			        * light deflection */
        public const int SEFLG_EQUATORIAL = (2 * 1024);    /* equatorial positions are wanted */
        public const int SEFLG_XYZ = (4 * 1024);     /* cartesian, not polar, coordinates */
        public const int SEFLG_RADIANS = (8 * 1024);     /* coordinates in radians, not degrees */
        public const int SEFLG_BARYCTR = (16 * 1024);    /* barycentric position */
        public const int SEFLG_TOPOCTR = (32 * 1024);    /* topocentric position */
        public const int SEFLG_ORBEL_AA = SEFLG_TOPOCTR; /* used for Astronomical Almanac mode in 
                                      * calculation of Kepler elipses */
        public const int SEFLG_SIDEREAL = (64 * 1024);    /* sidereal position */
        public const int SEFLG_ICRS = (128 * 1024);   /* ICRS (DE406 reference frame) */
        public const int SEFLG_DPSIDEPS_1980 = (256 * 1024); /* reproduce JPL Horizons 
                                      * 1962 - today to 0.002 arcsec. */
        public const int SEFLG_JPLHOR = SEFLG_DPSIDEPS_1980;
        public const int SEFLG_JPLHOR_APPROX = (512 * 1024);   /* approximate JPL Horizons 1962 - today */

        public const int SE_SIDBITS = 256;
        /* for projection onto ecliptic of t0 */
        public const int SE_SIDBIT_ECL_T0 = 256;
        /* for projection onto solar system plane */
        public const int SE_SIDBIT_SSY_PLANE = 512;
        /* with user-defined ayanamsha, t0 is UT */
        public const int SE_SIDBIT_USER_UT = 1024;

        /* sidereal modes (ayanamsas) */
        public const int SE_SIDM_FAGAN_BRADLEY = 0;
        public const int SE_SIDM_LAHIRI = 1;
        public const int SE_SIDM_DELUCE = 2;
        public const int SE_SIDM_RAMAN = 3;
        public const int SE_SIDM_USHASHASHI = 4;
        public const int SE_SIDM_KRISHNAMURTI = 5;
        public const int SE_SIDM_DJWHAL_KHUL = 6;
        public const int SE_SIDM_YUKTESHWAR = 7;
        public const int SE_SIDM_JN_BHASIN = 8;
        public const int SE_SIDM_BABYL_KUGLER1 = 9;
        public const int SE_SIDM_BABYL_KUGLER2 = 10;
        public const int SE_SIDM_BABYL_KUGLER3 = 11;
        public const int SE_SIDM_BABYL_HUBER = 12;
        public const int SE_SIDM_BABYL_ETPSC = 13;
        public const int SE_SIDM_ALDEBARAN_15TAU = 14;
        public const int SE_SIDM_HIPPARCHOS = 15;
        public const int SE_SIDM_SASSANIAN = 16;
        public const int SE_SIDM_GALCENT_0SAG = 17;
        public const int SE_SIDM_J2000 = 18;
        public const int SE_SIDM_J1900 = 19;
        public const int SE_SIDM_B1950 = 20;
        public const int SE_SIDM_SURYASIDDHANTA = 21;
        public const int SE_SIDM_SURYASIDDHANTA_MSUN = 22;
        public const int SE_SIDM_ARYABHATA = 23;
        public const int SE_SIDM_ARYABHATA_MSUN = 24;
        public const int SE_SIDM_SS_REVATI = 25;
        public const int SE_SIDM_SS_CITRA = 26;
        public const int SE_SIDM_TRUE_CITRA = 27;
        public const int SE_SIDM_TRUE_REVATI = 28;
        public const int SE_SIDM_TRUE_PUSHYA = 29;
        public const int SE_SIDM_GALCENT_RGILBRAND = 30;
        public const int SE_SIDM_GALEQU_IAU1958 = 31;
        public const int SE_SIDM_GALEQU_TRUE = 32;
        public const int SE_SIDM_GALEQU_MULA = 33;
        public const int SE_SIDM_GALALIGN_MARDYKS = 34;
        public const int SE_SIDM_TRUE_MULA = 35;
        public const int SE_SIDM_GALCENT_MULA_WILHELM = 36;
        public const int SE_SIDM_ARYABHATA_522 = 37;
        public const int SE_SIDM_BABYL_BRITTON = 38;
        //#define SE_SIDM_MANJULA         38
        public const int SE_SIDM_USER = 255; /* user-defined ayanamsha, t0 is TT */

        public const int SE_NSIDM_PREDEF = 39;

        /* used for swe_nod_aps(): */
        public const int SE_NODBIT_MEAN = 1;   /* mean nodes/apsides */
        public const int SE_NODBIT_OSCU = 2;   /* osculating nodes/apsides */
        public const int SE_NODBIT_OSCU_BAR = 4;   /* same, but motion about solar system barycenter is considered */
        public const int SE_NODBIT_FOPOINT = 256;   /* focal point of orbit instead of aphelion */

        /* default ephemeris used when no ephemeris flagbit is set */
        public const int SEFLG_DEFAULTEPH = SEFLG_SWIEPH;

        public const int SE_MAX_STNAME = 256;   /* maximum size of fixstar name;
                                         * the parameter star in swe_fixstar
                     * must allow twice this space for
				         * the returned star name.
					 */

        /* defines for eclipse computations */

        public const int SE_ECL_CENTRAL = 1;
        public const int SE_ECL_NONCENTRAL = 2;
        public const int SE_ECL_TOTAL = 4;
        public const int SE_ECL_ANNULAR = 8;
        public const int SE_ECL_PARTIAL = 16;
        public const int SE_ECL_ANNULAR_TOTAL = 32;
        public const int SE_ECL_PENUMBRAL = 64;
        public const int SE_ECL_ALLTYPES_SOLAR = (SE_ECL_CENTRAL | SE_ECL_NONCENTRAL | SE_ECL_TOTAL | SE_ECL_ANNULAR | SE_ECL_PARTIAL | SE_ECL_ANNULAR_TOTAL);
        public const int SE_ECL_ALLTYPES_LUNAR = (SE_ECL_TOTAL | SE_ECL_PARTIAL | SE_ECL_PENUMBRAL);
        public const int SE_ECL_VISIBLE = 128;
        public const int SE_ECL_MAX_VISIBLE = 256;
        public const int SE_ECL_1ST_VISIBLE = 512;  /* begin of partial eclipse */
        public const int SE_ECL_PARTBEG_VISIBLE = 512;  /* begin of partial eclipse */
        public const int SE_ECL_2ND_VISIBLE = 1024; /* begin of total eclipse */
        public const int SE_ECL_TOTBEG_VISIBLE = 1024;  /* begin of total eclipse */
        public const int SE_ECL_3RD_VISIBLE = 2048;    /* end of total eclipse */
        public const int SE_ECL_TOTEND_VISIBLE = 2048;    /* end of total eclipse */
        public const int SE_ECL_4TH_VISIBLE = 4096;    /* end of partial eclipse */
        public const int SE_ECL_PARTEND_VISIBLE = 4096;    /* end of partial eclipse */
        public const int SE_ECL_PENUMBBEG_VISIBLE = 8192;    /* begin of penumbral eclipse */
        public const int SE_ECL_PENUMBEND_VISIBLE = 16384;   /* end of penumbral eclipse */
        public const int SE_ECL_OCC_BEG_DAYLIGHT = 8192;    /* occultation begins during the day */
        public const int SE_ECL_OCC_END_DAYLIGHT = 16384;   /* occultation ends during the day */
        public const int SE_ECL_ONE_TRY = (32 * 1024);
        /* check if the next conjunction of the moon with
		 * a planet is an occultation; don't search further */

        /* for swe_rise_transit() */
        public const int SE_CALC_RISE = 1;
        public const int SE_CALC_SET = 2;
        public const int SE_CALC_MTRANSIT = 4;
        public const int SE_CALC_ITRANSIT = 8;
        public const int SE_BIT_DISC_CENTER = 256; /* to be or'ed to SE_CALC_RISE/SET,
				     * if rise or set of disc center is 
				     * required*/
        public const int SE_BIT_DISC_BOTTOM = 8192; /* to be or'ed to SE_CALC_RISE/SET,
                                      * if rise or set of lower limb of
                      * disc is requried*/
        public const int SE_BIT_NO_REFRACTION = 512; /* to be or'ed to SE_CALC_RISE/SET, 
				     * if refraction is to be ignored*/
        public const int SE_BIT_CIVIL_TWILIGHT = 1024; /* to be or'ed to SE_CALC_RISE/SET */
        public const int SE_BIT_NAUTIC_TWILIGHT = 2048; /* to be or'ed to SE_CALC_RISE/SET */
        public const int SE_BIT_ASTRO_TWILIGHT = 4096; /* to be or'ed to SE_CALC_RISE/SET */
        public const int SE_BIT_FIXED_DISC_SIZE = (16 * 1024); /* or'ed to SE_CALC_RISE/SET:
                                     * neglect the effect of distance on
				     * disc size */


        /* for swe_azalt() and swe_azalt_rev() */
        public const int SE_ECL2HOR = 0;
        public const int SE_EQU2HOR = 1;
        public const int SE_HOR2ECL = 0;
        public const int SE_HOR2EQU = 1;

        /* for swe_refrac() */
        public const int SE_TRUE_TO_APP = 0;
        public const int SE_APP_TO_TRUE = 1;

        /*
         * only used for experimenting with various JPL ephemeris files
         * which are available at Astrodienst's internal network
         */
        public const int SE_DE_NUMBER = 431;
        public const string SE_FNAME_DE200 = "de200.eph";
        public const string SE_FNAME_DE403 = "de403.eph";
        public const string SE_FNAME_DE404 = "de404.eph";
        public const string SE_FNAME_DE405 = "de405.eph";
        public const string SE_FNAME_DE406 = "de406.eph";
        public const string SE_FNAME_DE431 = "de431.eph";
        public const string SE_FNAME_DFT = SE_FNAME_DE431;
        public const string SE_FNAME_DFT2 = SE_FNAME_DE406;
        public const string SE_STARFILE_OLD = "fixstars.cat";
        public const string SE_STARFILE = "sefstars.txt";
        public const string SE_ASTNAMFILE = "seasnam.txt";
        public const string SE_FICTFILE = "seorbel.txt";
        #endregion

        /// <summary>
        /// Get the library version
        /// </summary>
        /// <returns>Number represents version</returns>
        [DllImport(SwephDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "swe_version")]
        private extern static IntPtr _swe_version(StringBuilder buff);
        public static string SweVersion()
        {
            StringBuilder buffer = new StringBuilder(64);
            _swe_version(buffer);
            return buffer.ToString();
        }

        [DllImport(SwephDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "swe_julday")]
        public extern static double SweJulday(int year, int month, int day, double hour, int gregflag);

        [DllImport(SwephDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "swe_revjul")]
        public extern static void SweRevjul(double jd, int gregflag, ref int year, ref int mon, ref int mday, ref double hour);

        [DllImport(SwephDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "swe_get_planet_name")]
        private extern static IntPtr _swe_get_planet_name(int ipl, StringBuilder spname);
        public static string SweGetPlanetName(int ipl)
        {
            StringBuilder buffer = new StringBuilder(255);
            _swe_get_planet_name(ipl, buffer);
            return buffer.ToString();
        }

        [DllImport(SwephDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "swe_calc_ut")]
        private extern static int _swe_calc_ut(double tjd_ut, int ipl, int iflag, double[] xx, StringBuilder serr);
        public static int SweCalcUT(double tjd_ut, int ipl, int iflag, ref double[] xx, ref string serr)
        {
            xx = new double[32];
            StringBuilder buffer = new StringBuilder(2048);
            var res = _swe_calc_ut(tjd_ut, ipl, iflag, xx, buffer);
            serr = buffer.ToString();
            return res;
        }

        [DllImport(SwephDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "swe_calc")]
        private extern static int _swe_calc(double tjd, int ipl, int iflag, double[] xx, StringBuilder serr);
        public static int SweCalc(double tjd, int ipl, int iflag, ref double[] xx, ref string serr)
        {
            xx = new double[32];
            StringBuilder buffer = new StringBuilder(2048);
            var res = _swe_calc(tjd, ipl, iflag, xx, buffer);
            serr = buffer.ToString();
            return res;
        }

        [DllImport(SwephDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "swe_deltat")]
        public extern static double SweDeltaT(double tjd);

        [DllImport(SwephDllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "swe_set_topo")]
        public extern static void SweSetTopo(double geolon, double geolat, double height);
    }

}
