using System;

public class Member
{
    private int mId;
    private string name;
    private string password;

    public Member(int mId, string name, string password)
    {
        this.mId = mId;
        this.name = name;
        this.password = password;
    }

    public int getMId() { return mId; }
    public void setMId(int mId) { this.mId = mId; }
    public string getName() { return name; }
    public void setName(string name) { this.name = name; }
    public string getPassword() { return password; }
    public void setPassword(string password) { this.password = password; }
}
