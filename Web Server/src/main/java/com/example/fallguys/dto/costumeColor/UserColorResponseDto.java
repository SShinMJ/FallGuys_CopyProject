package com.example.fallguys.dto.costumeColor;

import com.example.fallguys.domain.CostumeColor;
import com.example.fallguys.domain.User;
import com.example.fallguys.domain.UserCostumeColor;
import lombok.Getter;

@Getter
public class UserColorResponseDto {
    private final User user;
    private final CostumeColor colorList;
    private final int userKudos;
    private final int userCostumeColor;

    public UserColorResponseDto(UserCostumeColor userCostumeColor) {
        this.user = userCostumeColor.getUser();
        this.colorList = userCostumeColor.getCostumeColor();
        this.userKudos = user.getUserKudos();
        this.userCostumeColor = user.getUserCostumeColor();
    }
}