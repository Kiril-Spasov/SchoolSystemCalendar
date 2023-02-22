class SchoolCalendar
{
    private int _schoolMonth;
    private int _schoolDay;
    private int _minAge;

    public SchoolCalendar(int sm, int sd, int ma)
    {
        _schoolMonth = sm;
        _schoolDay = sd;
        _minAge = ma;
    }

    //We are defining dates in the format 18900229 where 1890 is the year, 02 is month, 29 is day,
    //as the course's intend is to show the time before objects existed.
    public int GetSchoolDate(int birthDate)
    {
        int firstDayAtSchool = (birthDate / 10000 + _minAge) * 10000 + _schoolMonth * 100 + _schoolDay;

        if (birthDate % 10000 > _schoolMonth * 100 + _schoolDay)
        {
            firstDayAtSchool += 10000;
        }

        return firstDayAtSchool;
    }

    //Leap year defined by the Gregorian calendar
    public virtual bool IsLeap(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
    }

    //Finds 1st birthday celebrated at school
    public int GetSchoolBirthDay(int birthDate)
    {
        int schoolDate = GetSchoolDate(birthDate);

        int birthDayYear = schoolDate / 10000;

        if (birthDate % 10000 < schoolDate % 10000)
        {
            birthDayYear = birthDayYear + 1;
        }

        //If birthdate is feb 29
        if (birthDate % 10000 == 2 * 100 + 29 &&
            !IsLeap(birthDayYear))
        {
            //Programmatic way to round up to the first number divisible by 4
            birthDayYear = (birthDayYear + 3) / 4 * 4;

            if (!IsLeap(birthDayYear))
            {
                birthDayYear = birthDayYear + 4;
            }
        }

        int firstBirthDayAtSchool = birthDayYear * 10000 + birthDate % 10000;

        return firstBirthDayAtSchool;
    }
}

//If we have the case were we need to make calculations based on the Julian calendar
class JulianSchoolCalendar : SchoolCalendar
{
    public JulianSchoolCalendar(int sm, int sd, int ma) : base(sm, sd, ma)
    {
        
    }
    //Leap year defined by the Julian calendar
    public override bool IsLeap(int year)
    {
        return year % 4 == 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int birthDate = 18900229;

        //Get by the Gregorian calendar
        SchoolCalendar calendar1 = new SchoolCalendar(9, 1, 6);

        int schoolDate = calendar1.GetSchoolDate(birthDate);
        int schoolBirthday = calendar1.GetSchoolBirthDay(birthDate);

        Console.WriteLine("1st day at school: " + schoolDate + ", 1st birthday at school by Gregorian calendar: " + schoolBirthday);

        //Get by the Julian calendar
        SchoolCalendar calendar2 = new JulianSchoolCalendar(9, 15, 7);

        int alternativeSchoolDate = calendar2.GetSchoolDate(birthDate);
        int alternativeSchoolBirthday = calendar2.GetSchoolBirthDay(birthDate);

        Console.WriteLine("1st day at school: " + alternativeSchoolDate + ", 1st birthday at school by Julian calendar: " + alternativeSchoolBirthday);

        Console.ReadLine();
    }
}


