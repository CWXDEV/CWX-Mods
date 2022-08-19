import { DependencyContainer } from "tsyringe";
import { IPreAkiLoadMod } from "@spt-aki/models/external/IPreAkiLoadMod";

class CWX_MasterKey implements IPreAkiLoadMod
{
    private pkg;

    public preAkiLoad(container: DependencyContainer): void
    { 
		this.pkg = require("../package.json")
    }
}

module.exports = { mod: new CWX_MasterKey() }