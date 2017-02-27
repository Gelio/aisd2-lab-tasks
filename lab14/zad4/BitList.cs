
namespace ASD
{
using System.Collections.Generic;
using System.Text;

public class BitList
    {

    // UWAGA:  Każdy bajt to 8 bitów !!!
    private List<byte> bits;

    public BitList()
        {
        bits = new List<byte>();
        Count = 0;
        return;
        }

    public BitList(BitList bl)
        {
        bits = new List<byte>(bl.bits);
        Count = bl.Count;
        return;
        }

    public BitList(string str)
        {
        bits = new List<byte>(str.Length<<3);
        for ( int i=0 ; i<str.Length ; ++i )
            Append(str[i]!='0');
        }

    // UWAGA:  To liczba zapamiętanych bitów, a nie liczba elementów listy bits (bo każdy element to bajt!)
    public int Count { get; private set; }

    public bool this[int index]
        {
        get {
            if ( index<0 || index>=Count ) throw new System.IndexOutOfRangeException();
            return ( bits[index>>3] & (1<<(index&7)) ) !=0 ;
            }
        set {
            if ( index<0 || index>=Count ) throw new System.IndexOutOfRangeException();
            if ( value )
                bits[index>>3] |= (byte)(1<<(index&7));
            else
                bits[index>>3] &= (byte)~(1<<(index&7));
            }
        }

    public BitList Append(bool b)
        {
        if ( (Count&0x7)==0 ) bits.Add(0);
        ++Count;
        this[Count-1]=b;
        return this;
        }

    public BitList Append(BitList bl)
        {
        for ( int i=0 ; i<bl.Count ; ++i )
            Append(bl[i]);
        return this;
        }

    public override string ToString()
        {
        StringBuilder sb = new StringBuilder();
        for ( int i=0 ; i<Count ; ++i )
            sb.Append(this[i]?'1':'0');
        return sb.ToString();
        }

    public override bool Equals(object obj)
        {
        BitList b = obj as BitList;
        if ( b==null )
            return false;
        if ( bits.Count!=b.bits.Count )
            return false;
        for ( int i=0 ; i<bits.Count ; ++i )
            if ( bits[i]!=b.bits[i] )
                return false;
        return true;
        }

    public override int GetHashCode()
        {
        int res=0;
        for ( int i=0 ; i<bits.Count ; ++i )
            res ^= ((int)bits[i])<<((i&3)<<3);
        return res;
        }

    }  // class BitList

}  // namespace ASD.Graphs
