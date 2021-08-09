/*
 * Copyright (c) Huawei Technologies Co., Ltd. 2019-2020. All rights reserved.
 * Generated by the CloudDB ObjectType compiler.  DO NOT EDIT!
 */
package com.clouddbdemo.kb.huawei;

import com.huawei.agconnect.cloud.database.CloudDBZoneObject;
import com.huawei.agconnect.cloud.database.Text;
import com.huawei.agconnect.cloud.database.annotations.DefaultValue;
import com.huawei.agconnect.cloud.database.annotations.EntireEncrypted;
import com.huawei.agconnect.cloud.database.annotations.NotNull;
import com.huawei.agconnect.cloud.database.annotations.Indexes;
import com.huawei.agconnect.cloud.database.annotations.PrimaryKeys;

import java.util.Date;

/**
 * Definition of ObjectType ChatAppOT.
 *
 * @since 2021-08-08
 */
@PrimaryKeys({"id"})
@Indexes({"date:date", "shadowFlag:shadowFlag", "id:id", "message:message", "userId:userId"})
public final class ChatAppOT extends CloudDBZoneObject {
    private String id;

    private String userId;

    private String message;

    private Date date;

    @DefaultValue(booleanValue = true)
    private Boolean shadowFlag;

    public ChatAppOT() {
        super(ChatAppOT.class);
        this.shadowFlag = true;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getId() {
        return id;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public String getUserId() {
        return userId;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public String getMessage() {
        return message;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public Date getDate() {
        return date;
    }

    public void setShadowFlag(Boolean shadowFlag) {
        this.shadowFlag = shadowFlag;
    }

    public Boolean getShadowFlag() {
        return shadowFlag;
    }

}
