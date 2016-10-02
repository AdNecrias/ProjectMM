namespace AdNecriasMeldowMethod {
    public enum EntityType {
        zombie_default,
        zombie_fast,
        zombie_tank
    }

    public enum RarityLevel {
        unique,     //.05
        legendary,  //.45
        rare,       //.20
        uncommon,   //.25
        common      //.50
    }

    //https://en.wikipedia.org/wiki/Homeland_Security_Advisory_System
    //In the United States, the Homeland Security Advisory System was a color-coded terrorism threat advisory scale.
    //Red, Orange, Yellow, Blue and Green, in that order from Higher values to Lower.
    public enum ThreatLevel {
        severe,
        high,
        elevated,
        guarded,    //nothing should happen
        low
    }

}