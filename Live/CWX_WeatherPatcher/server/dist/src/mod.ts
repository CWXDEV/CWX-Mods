import { DependencyContainer } from "tsyringe";
import { IPreAkiLoadMod } from "@spt-aki/models/external/IPreAkiLoadMod";

class CWX_WeatherPatcher implements IPreAkiLoadMod
{
    public preAkiLoad(container: DependencyContainer): void
    { 
    }
}

module.exports = { mod: new CWX_WeatherPatcher() }