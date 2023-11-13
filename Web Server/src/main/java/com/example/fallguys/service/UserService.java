package com.example.fallguys.service;

import com.example.fallguys.domain.*;
import com.example.fallguys.dto.SuccessResponseDto;
import com.example.fallguys.dto.user.*;
import com.example.fallguys.exception.BaseException;
import com.example.fallguys.exception.BaseResponseCode;
import com.example.fallguys.repository.*;
import com.example.fallguys.util.JwtTokenProvider;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class UserService {
    private final PasswordEncoder passwordEncoder;
    private final JwtTokenProvider jwtTokenProvider;

    private final UserRepository userRepository;

    public Long signUp(UserSignupRequestDto userSignupRequestDto) throws BaseException {
        userSignupRequestDto.setUserPassword(passwordEncoder.encode(userSignupRequestDto.getUserPassword()));

        Long userNumber;
        try {
            userNumber = userRepository.save(userSignupRequestDto.toEntity()).getUserNumber();
        } catch (Exception e) {
            throw new BaseException(BaseResponseCode.FAILED_TO_SAVE_USER);
        }
        return userNumber;
    }

    public SuccessResponseDto nicknameCheck(String nickname) {
        boolean exitsUserCheck = userRepository.existsByUserNickname(nickname).orElseThrow(() -> new BaseException(BaseResponseCode.USER_NOT_FOUND));

        if (exitsUserCheck) {
            throw new BaseException(BaseResponseCode.DUPLICATE_NICKNAME);
        }

        return new SuccessResponseDto(HttpStatus.OK);
    }
    public User findUserById(String userId){
        return userRepository.findByUserId(userId).orElseThrow(() -> new BaseException(BaseResponseCode.USER_NOT_FOUND));
    }

    public UserLoginResponseDto login(UserLoginRequestDto userLoginRequestDto) {

        User user = findUserById(userLoginRequestDto.getUserId());
        if (!passwordEncoder.matches(userLoginRequestDto.getUserPassword(), user.getUserPassword()))
            throw new BaseException(BaseResponseCode.INVALID_PASSWORD);

        String token = jwtTokenProvider.createToken(userLoginRequestDto.getUserId());
        return new UserLoginResponseDto(HttpStatus.OK, token);
    }

    public User findUserByToken(){
        return userRepository.findByUserId(SecurityContextHolder.getContext()
                .getAuthentication().getName())
                .orElseThrow(() -> new BaseException(BaseResponseCode.USER_NOT_FOUND));
    }

    public UserResponseDto findByUser() {
        return new UserResponseDto(findUserByToken());
    }


    public SuccessResponseDto updateNickname(UserUpdateNameRequestDto userUpdateNameDto) {
        User user = findUserByToken();

        user.setUserNickname(userUpdateNameDto.getUserNickname());
        userRepository.save(user);

        return new SuccessResponseDto(HttpStatus.OK);
    }
}
