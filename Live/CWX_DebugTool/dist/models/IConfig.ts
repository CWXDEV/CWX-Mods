export interface IConfig
{
    showLogs: boolean
    globalsConfig: globalsConfig
    ragfairConfig: ragfairConfig
    locationConfig: locationConfig
    inraidConfig: inraidConfig
    itemsConfig: itemsConfig
}

export interface globalsConfig
{
    noFallDamage: boolean
    openFlea: boolean
    quickScav: boolean
}

export interface ragfairConfig
{
    staticTrader: boolean
    roublesOnly: boolean
    disableBSGBlacklist: boolean
}

export interface locationConfig
{
    turnLootOff: boolean
}

export interface inraidConfig
{
    turnPVEOff: boolean
}

export interface itemsConfig
{
    changeShrapProps: boolean
    changeMaxAmmoForKS23: boolean
}