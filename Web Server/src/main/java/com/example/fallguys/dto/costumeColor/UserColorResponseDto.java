package com.example.fallguys.dto.costumeColor;

import com.example.fallguys.domain.UserCostumeColor;
import lombok.Getter;

@Getter
public class UserColorResponseDto {
    private final Long colorId;
    private final boolean isOwn;

    public UserColorResponseDto(UserCostumeColor userCostumeColor) {
        this.colorId= userCostumeColor.getCostumeColor().getCostumeColorNumber();
        this.isOwn = userCostumeColor.isOwn();
    }
}