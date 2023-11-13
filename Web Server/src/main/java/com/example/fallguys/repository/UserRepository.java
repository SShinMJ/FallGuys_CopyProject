package com.example.fallguys.repository;

import com.example.fallguys.domain.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {
    Optional<User> findByUserId(String id);
    Optional<User> findByUserNumber(Long userNumber);

    Optional<Boolean> existsByUserId(String id);
    Optional<Boolean> existsByUserNickname(String nickname);
}
