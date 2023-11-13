package com.example.fallguys.dto.user;

import com.example.fallguys.domain.User;
import lombok.Getter;

@Getter
public class UserAdminResponseDto {
    private final Long userNumber;
    private final String userId;
    private final String userNickname;

    public UserAdminResponseDto(User user) {
        this.userNumber = user.getUserNumber();
        this.userId = user.getUserId();
        this.userNickname = user.getUserNickname();
    }
}
