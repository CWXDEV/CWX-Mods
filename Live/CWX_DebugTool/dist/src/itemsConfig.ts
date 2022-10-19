import { inject, injectable } from "tsyringe";

import { CwxConfigHandler } from "./configHandler";
import { DatabaseServer } from "@spt-aki/servers/DatabaseServer";
import { ITemplateItem } from "@spt-aki/models/eft/common/tables/ITemplateItem";
import { IItemConfig } from "@spt-aki/models/spt/config/IItemConfig"
import { ItemsConfig } from "models/IConfig";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";
import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";

@injectable()
export class CwxItemsConfig
{
    private tables: Record<string, ITemplateItem>;
    private config: ItemsConfig;
    private itemConfig: IItemConfig;
    
    constructor(
        @inject("DatabaseServer") private databaseServer: DatabaseServer,
        @inject("ConfigServer") private configServer: ConfigServer,
        @inject("CwxConfigHandler") private configHandler: CwxConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.tables = this.databaseServer.getTables().templates.items;
        this.itemConfig = this.configServer.getConfig(ConfigTypes.ITEM);
        this.config = this.configHandler.getConfig().itemsConfig;

        this.changeShrapProps();
        this.changeMaxAmmoForKS23();
        this.removeDevFromBlacklist();
    }
    

    private changeShrapProps(): void
    {
        const shrap = this.tables["5e85a9a6eacf8c039e4e2ac1"];

        if (this.config.changeShrapProps)
        {
            shrap._props.Damage = 200;
            shrap._props.InitialSpeed = 1000;
        }
    }

    private changeMaxAmmoForKS23(): void
    {
        const ks23 = this.tables["5f647d9f8499b57dc40ddb93"];

        if (this.config.changeMaxAmmoForKS23)
        {
            ks23._props.Cartridges[0]._max_count = 30;
        }
    }
    
    private removeDevFromBlacklist(): void
    {
        if (this.config.removeDevFromBlacklist)
        {
            this.itemConfig.blacklist.splice(this.itemConfig.blacklist.indexOf("58ac60eb86f77401897560ff"));
        }
    }
}