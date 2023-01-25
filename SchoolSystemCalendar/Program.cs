class SchoolCalendar
{
    int schoolMonth;
    int schoolDay;
    int minAge;

    public SchoolCalendar(int sm, int sd, int ma)
    {
        schoolMonth = sm;
        schoolDay = sd;
        minAge = ma;
    }

    public int GetSchoolDate(int birthDate)
    {
        int firstDayAtSchool = (birthDate / 10000 + minAge) * 10000 + schoolMonth * 100 + schoolDay;

        if (birthDate % 10000 > schoolMonth * 100 + schoolDay)
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

        //Dates by the Gregorian calendar
        SchoolCalendar calendar1 = new SchoolCalendar(9, 1, 6);

        int schoolDate = calendar1.GetSchoolDate(birthDate);
        int schoolBirthday = calendar1.GetSchoolBirthDay(birthDate);

        Console.WriteLine("1st day at school: " + schoolDate + ", 1st birthday at school by Gregorian calendar: " + schoolBirthday);

        //Dates by the Julian calendar
        SchoolCalendar calendar2 = new JulianSchoolCalendar(9, 15, 7);

        int alternativeSchoolDate = calendar2.GetSchoolDate(birthDate);
        int alternativeSchoolBirthday = calendar2.GetSchoolBirthDay(birthDate);

        Console.WriteLine("1st day at school: " + alternativeSchoolDate + ", 1st birthday at school by Julian calendar: " + alternativeSchoolBirthday);

        Console.ReadLine();
    }
}


