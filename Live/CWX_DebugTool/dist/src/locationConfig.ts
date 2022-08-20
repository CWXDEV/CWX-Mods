import { inject, injectable } from "tsyringe";

import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";
import { ILocationConfig } from "@spt-aki/models/spt/config/ILocationConfig";

import { CWX_ConfigHandler } from "./configHandler";
import { locationConfig } from "models/IConfig";

@injectable()
export class CWX_LocationConfig
{
    private tables: ILocationConfig;
    private config: locationConfig;
    
    constructor(
        @inject("ConfigServer") private configServer: ConfigServer,
        @inject("CWX_ConfigHandler") private configHandler: CWX_ConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().locationConfig;
        this.tables = this.configServer.getConfig(ConfigTypes.LOCATION);

        this.TurnLootOff();
    }

    private TurnLootOff(): void
    {
        if (this.config.turnLootOff)
        {
            for (const location in this.tables.looseLootMultiplier) {
                this.tables.looseLootMultiplier[location] = 0;
            }

            for (const location in this.tables.staticLootMultiplier) {
                this.tables.staticLootMultiplier[location] = 0;
            }
        }
    }
}