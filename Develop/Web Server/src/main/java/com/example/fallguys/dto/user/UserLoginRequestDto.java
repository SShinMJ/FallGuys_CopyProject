package com.example.fallguys.dto.user;

import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
public class UserLoginRequestDto {
    private String userId;
    private String userPassword;

    @Builder
    public UserLoginRequestDto(String userId, String userPassword) {
        this.userId = userId;
        this.userPassword = userPassword;
    }
}