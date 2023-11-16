package com.example.fallguys.dto.user;

import com.example.fallguys.domain.User;
import lombok.Getter;

@Getter
public class UserResponseDto {
    private final String userId;
    private final String userNickname;
    private final int userKudos;
    private final Long userCostumeColor;

    public UserResponseDto(User user) {
        this.userId = user.getUserId();
        this.userNickname = user.getUserNickname();
        this.userKudos = user.getUserKudos();
        this.userCostumeColor = user.getUserCostumeColor();
    }
}